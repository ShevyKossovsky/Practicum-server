using Server.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Core.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string IdNumber { get; set; }

        public string Gender { get; set; }

        public DateTime EmploymentStartDate { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool IsActive { get; set; }

        public List<EmployeePositionDto>? PositionsList { get; set; }


    }
}
