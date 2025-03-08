using System.Data;

namespace ManagementSystem.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        // קשרים
        public List<WorkHours>? WorkHours { get; set; }
        public List<LeaveRequest>? LeaveRequests { get; set; }
        public int RoleId { get; set; } // Foreign Key
        public Role? Role { get; set; }
    }
}
