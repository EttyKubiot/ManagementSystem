
using ManagementSystem.Data.Repositories;
using ManagementSystem.Data;

namespace ManagementSystem.Services
{
    public class LeaveRequestService
    {
        private readonly LeaveRequestRepository _leaveRequestRepository;

        public LeaveRequestService(LeaveRequestRepository leaveRequestRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsByUserIdAsync(int userId)
        {
            return await _leaveRequestRepository.GetLeaveRequestsByUserIdAsync(userId);
        }

        public async Task<LeaveRequest> AddLeaveRequestAsync(LeaveRequest leaveRequest)
        {
            return await _leaveRequestRepository.AddLeaveRequestAsync(leaveRequest);
        }

        public async Task<bool> DeleteLeaveRequestAsync(int id)
        {
            return await _leaveRequestRepository.DeleteLeaveRequestAsync(id);
        }

        public async Task<LeaveRequest?> GetLeaveRequestByIdAsync(int id)
        {
            return await _leaveRequestRepository.GetLeaveRequestByIdAsync(id);
        }
        public async Task<bool> UpdateLeaveRequestAsync(LeaveRequest request)
        {
            return await _leaveRequestRepository.UpdateLeaveRequestAsync(request);
        }
    }
}
