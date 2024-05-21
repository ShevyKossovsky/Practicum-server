using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Server.Core.Entities;
using Server.Core.Repositories;
using Server.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;
        private readonly IPositionService _positionService; 


        public EmployeeRepository(DataContext context, IPositionService positionService)
        {
            _positionService = positionService;
            _context = context;
        }

        public async Task<List<Employee>> GetEmployeeAsync()
        {
            return await _context.EmployeesList
                                 .Include(e => e.PositionsList)
                                     .ThenInclude(p => p.Position)
                                 .Where(e => e.IsActive)
                                 .ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _context.EmployeesList
                                 .Include(e => e.PositionsList)
                                     .ThenInclude(p => p.Position)
                                 .FirstOrDefaultAsync(e => e.Id == id && e.IsActive);
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            if (!IsValidEmployee(employee))
            {
                throw new ArgumentException("Employee data is invalid. Please check the input.", nameof(employee));
            }

            _context.EmployeesList.Add(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        private bool IsValidEmployee(Employee employee)
        {
            // וודא שתאריך הלידה ותאריך תחילת העבודה אינם ריקים ותקינים
            if (employee.DateOfBirth == null || employee.EmploymentStartDate == null ||
                employee.DateOfBirth == DateTime.MinValue || employee.EmploymentStartDate == DateTime.MinValue)
            {
                return false;
            }

            // וודא שהשם הראשון והשם המשפחה אינם ריקים
            if (string.IsNullOrEmpty(employee.FirstName) || string.IsNullOrEmpty(employee.LastName))
            {
                return false;
            }

            // וודא שהמין מוגדר ותקין
            if (!Enum.IsDefined(typeof(Gender), employee.Gender))
            {
                return false;
            }

            // וודא אורך תעודת הזהות
            if (string.IsNullOrEmpty(employee.IdNumber) || employee.IdNumber.Length != 9)
            {
                return false;
            }

            // וודא כל תפקיד ברשימת התפקידים
            if (employee.PositionsList != null && employee.PositionsList.Count > 1)
            {
                var distinctPositions = employee.PositionsList
                    .Select(p => p.PositionId)
                    .Distinct()
                    .Count();

                if (distinctPositions != employee.PositionsList.Count)
                {
                    return false;
                }
            }

            foreach (var position in employee.PositionsList)
            {
                // וודא שהתפקיד קיים
                if (position == null || position.PositionId == 0)
                {
                    return false;
                }

                // וודא שתאריך הכניסה לתפקיד אחרי או באותו היום כמו תאריך ההתחלה לעבודה
                if (position.EntryDate == null || position.EntryDate < employee.EmploymentStartDate.Date)
                {
                    return false;
                }
            }

            return true;
        }


        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await _context.EmployeesList.FindAsync(id);
            if (employee != null)
            {
                employee.IsActive = false;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<Employee> UpdateEmployeeAsync(int id, Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee), "Employee object is null.");
            }

            if (!IsValidEmployee(employee)) // בדיקת ולידציה
            {
                throw new ArgumentException("Employee data is invalid. Please check the input.", nameof(employee));
            }

            var updateEmployee = await _context.EmployeesList
                .Include(e => e.PositionsList)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (updateEmployee == null || updateEmployee.IsActive == false)
            {
                throw new ArgumentException("Employee with the provided ID not found or is not active.", nameof(id));
            }

            updateEmployee.FirstName = employee.FirstName;
            updateEmployee.LastName = employee.LastName;
            updateEmployee.IdNumber = employee.IdNumber;
            updateEmployee.Gender = employee.Gender;
            updateEmployee.EmploymentStartDate = employee.EmploymentStartDate;
            updateEmployee.DateOfBirth = employee.DateOfBirth;
           

            updateEmployee.IsActive = employee.IsActive;

            // נקה את רשימת התפקידים הקיימת והוסף מחדש
            updateEmployee.PositionsList.Clear();

            foreach (var newPosition in employee.PositionsList)
            {
                var position = await _positionService.GetPositionByIdAsync(newPosition.PositionId);
                if (position != null)
                {
                    updateEmployee.PositionsList.Add(new EmployeePosition
                    {
                        Position = position,
                        IsManagement = newPosition.IsManagement,
                        EntryDate = newPosition.EntryDate
                    });
                }
            }

            // שמירת השינויים בבסיס הנתונים
            await _context.SaveChangesAsync();

            return updateEmployee;
        }

    }
}
