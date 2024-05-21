using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Server.Core.Entities
{
    public class EmployeePosition
    {
        public int Id { get; set; }
        public Position Position { get; set; }
        public int PositionId { get; set; }
        public bool IsManagement { get; set; }
        public DateTime EntryDate { get; set; }


       
    }
}
