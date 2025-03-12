using ManagementSystem.Data.Entities;

namespace ManagementSystem.Data
{
    public class LeaveRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Foreign Key
        public User? User { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Reason { get; set; }
        public bool IsApproved { get; set; }
      
    }
}
