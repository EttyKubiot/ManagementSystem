using ManagementSystem.Data.Entities;

namespace ManagementSystem.Data
{
    public class WorkHours
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Foreign Key
        public User User { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
