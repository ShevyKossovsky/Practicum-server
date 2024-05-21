using Server.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Core.DTOs
{
    public class EmployeePositionDto
    {
        public PositionDto Position { get; set; }
        public bool IsManagement { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
