using Microsoft.AspNetCore.Mvc;
using ManagementSystem.Services;
using ManagementSystem.Data.Entities;
using ManagementSystem.Data;
using System.Security.Claims;

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
            var workHours = await _workHoursService.GetWorkHoursByIdAsync(id);
            if (workHours == null) return NotFound();

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (workHours.UserId != userId)
            {
                return Forbid(); // ❌ אי אפשר למחוק שעות של מישהו אחר!
            }

            var deleted = await _workHoursService.DeleteWorkHoursAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
