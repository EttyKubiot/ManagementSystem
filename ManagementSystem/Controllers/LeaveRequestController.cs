using Microsoft.AspNetCore.Mvc;
using ManagementSystem.Services;
using ManagementSystem.Data.Entities;
using ManagementSystem.Data;

namespace ManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly LeaveRequestService _leaveRequestService;

        public LeaveRequestController(LeaveRequestService leaveRequestService)
        {
            _leaveRequestService = leaveRequestService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserLeaveRequests(int userId)
        {
            var leaveRequests = await _leaveRequestService.GetLeaveRequestsByUserIdAsync(userId);
            return Ok(leaveRequests);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitLeaveRequest([FromBody] LeaveRequest leaveRequest)
        {
            var newRequest = await _leaveRequestService.AddLeaveRequestAsync(leaveRequest);
            return Ok(newRequest);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelLeaveRequest(int id)
        {
            var deleted = await _leaveRequestService.DeleteLeaveRequestAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
