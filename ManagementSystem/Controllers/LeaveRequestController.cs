using Microsoft.AspNetCore.Mvc;
using ManagementSystem.Services;
using ManagementSystem.Data.Entities;
using ManagementSystem.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
            var request = await _leaveRequestService.GetLeaveRequestByIdAsync(id);
            if (request == null) return NotFound();

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (request.UserId != userId)
            {
                return Forbid(); // ❌ אי אפשר למחוק בקשות של מישהו אחר!
            }

            var deleted = await _leaveRequestService.DeleteLeaveRequestAsync(id);
            return deleted ? NoContent() : NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("approve/{id}")]
        public async Task<IActionResult> ApproveLeaveRequest(int id, [FromBody] bool isApproved)
        {

            var request = await _leaveRequestService.GetLeaveRequestByIdAsync(id);
            if (request == null) return NotFound();

            request.IsApproved = isApproved;  // ✅ נעדכן את הבקשה לפי החלטת המנהל
            await _leaveRequestService.UpdateLeaveRequestAsync(request);

            return Ok(request);
        }
    }
}
