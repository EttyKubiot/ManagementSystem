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
        public async Task<bool> UpdateLeaveRequestAsync(LeaveRequest request)
        {
            _context.LeaveRequests.Update(request);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<LeaveRequest?> GetLeaveRequestByIdAsync(int id)
        {
            return await _context.LeaveRequests.FirstOrDefaultAsync(l => l.Id == id);
        }

      


    }
}

