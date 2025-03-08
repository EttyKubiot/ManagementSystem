using ManagementSystem.Data;

namespace ManagementSystem.API.Models
{
    public class UserPostModel
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
       
        public int RoleId { get; set; }

    }
}
