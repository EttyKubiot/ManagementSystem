using Microsoft.EntityFrameworkCore;
using ManagementSystem.Data.Entities;

namespace ManagementSystem.Data.Repositories
{
    public class LeaveRequestRepository
    {
        private readonly DataContext _context;

        public LeaveRequestRepository(DataContext context)
        {
            _context = context;
        }

        // שליפת כל בקשות החופשה למשתמש מסוים
        public async Task<List<LeaveRequest>> GetLeaveRequestsByUserIdAsync(int userId)
        {
            return await _context.LeaveRequests
                .Where(lr => lr.UserId == userId)
                .ToListAsync();
        }

        // הוספת בקשת חופשה חדשה
        public async Task<LeaveRequest> AddLeaveRequestAsync(LeaveRequest leaveRequest)
        {
            _context.LeaveRequests.Add(leaveRequest);
            await _context.SaveChangesAsync();
            return leaveRequest;
        }

        // מחיקת בקשת חופשה לפי ID
        public async Task<bool> DeleteLeaveRequestAsync(int id)
        {
            var leaveRequest = await _context.LeaveRequests.FindAsync(id);
            if (leaveRequest == null) return false;

            _context.LeaveRequests.Remove(leaveRequest);
            await _context.SaveChangesAsync();
            return true;
        }

        // עדכון בקשת חופשה
        public async Task<LeaveRequest?> UpdateLeaveRequestAsync(LeaveRequest leaveRequest)
        {
            var existingRequest = await _context.LeaveRequests.FindAsync(leaveRequest.Id);
            if (existingRequest == null) return null;

            existingRequest.StartDate = leaveRequest.StartDate;
            existingRequest.EndDate = leaveRequest.EndDate;
            existingRequest.Reason = leaveRequest.Reason;
            existingRequest.IsApproved = leaveRequest.IsApproved;

            await _context.SaveChangesAsync();
            return existingRequest;
        }
    }
}

