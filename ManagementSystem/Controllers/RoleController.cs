using Microsoft.AspNetCore.Mvc;
using ManagementSystem.Services;
using ManagementSystem.Data.Entities;
using ManagementSystem.Data;

namespace ManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleService _roleService;

        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleService.GetRolesAsync();
            return Ok(roles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] Role role)
        {
            var newRole = await _roleService.CreateRoleAsync(role);
            return Ok(newRole);
        }
    }
}
