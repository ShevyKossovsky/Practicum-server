using Server.Core.Entities;

namespace server.Models
{
    public class EmployeePositionPostModel
    {
        public int PositionId { get; set; }
        public bool IsManagement { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
