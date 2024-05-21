using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Core.Entities
{
    public enum Gender
    {
        Male,
        Female
    
    }
    public class Employee
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string IdNumber { get; set; }

        public Gender Gender { get; set; }

        public DateTime EmploymentStartDate { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool IsActive { get; set; }=true;

        public List<EmployeePosition>? PositionsList { get; set; }
    }
      
}
