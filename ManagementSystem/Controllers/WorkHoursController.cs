using Microsoft.AspNetCore.Mvc;
using ManagementSystem.Services;
using ManagementSystem.Data.Entities;
using ManagementSystem.Data;

namespace ManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkHoursController : ControllerBase
    {
        private readonly WorkHoursService _workHoursService;

        public WorkHoursController(WorkHoursService workHoursService)
        {
            _workHoursService = workHoursService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserWorkHours(int userId)
        {
            var workHours = await _workHoursService.GetWorkHoursByUserIdAsync(userId);
            return Ok(workHours);
        }

        [HttpPost]
        public async Task<IActionResult> AddWorkHours([FromBody] WorkHours workHours)
        {
            var newWorkHours = await _workHoursService.AddWorkHoursAsync(workHours);
            return Ok(newWorkHours);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkHours(int id)
        {
            var deleted = await _workHoursService.DeleteWorkHoursAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
