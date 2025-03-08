using ManagementSystem.Data.Repositories;
using ManagementSystem.Data.Entities;
using ManagementSystem.Data;

namespace ManagementSystem.Services
{
    public class RoleService
    {
        private readonly RoleRepository _roleRepository;

        public RoleService(RoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        // שליפת כל התפקידים
        public async Task<List<Role>> GetRolesAsync()
        {
            return await _roleRepository.GetAllRolesAsync();
        }
        public async Task<Role?> GetRoleByIdAsync(int roleId)
        {
            return await _roleRepository.GetRoleByIdAsync(roleId);
        }


        // יצירת תפקיד חדש
        public async Task<Role> CreateRoleAsync(Role role)
        {
            return await _roleRepository.AddRoleAsync(role);
        }
    }
}
