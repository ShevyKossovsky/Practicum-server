using Server.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Core.Repositories
{
    public interface IEmployeeRepository
    {

        Task<List<Employee>> GetEmployeeAsync();

        Task<Employee> GetByIdAsync(int id);

        Task<Employee> AddEmployeeAsync(Employee employee);

        Task DeleteEmployeeAsync(int id);

        Task<Employee> UpdateEmployeeAsync(int id, Employee employee);

    }
}
