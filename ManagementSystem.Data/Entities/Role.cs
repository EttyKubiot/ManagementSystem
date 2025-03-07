using ManagementSystem.Data.Entities;

namespace ManagementSystem.Data
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // קשר של אחד לרבים עם משתמשים
        public List<User> Users { get; set; }
    }

}
