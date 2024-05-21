using Server.Core.Entities;

namespace server.Models
{
    public class EmployeePostModel
    {
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string IdNumber { get; set; }

        public string Gender { get; set; }

        public DateTime EmploymentStartDate { get; set; }

        public DateTime DateOfBirth { get; set; }

        public List<EmployeePositionPostModel> PositionsList { get; set; }
    }
}
