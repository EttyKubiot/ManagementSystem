using Microsoft.EntityFrameworkCore;
using ManagementSystem.Data.Entities;

namespace ManagementSystem.Data.Repositories
{
    public class WorkHoursRepository
    {
        private readonly DataContext _context;

        public WorkHoursRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<WorkHours>> GetWorkHoursByUserIdAsync(int userId)
        {
            return await _context.WorkHours.Where(wh => wh.UserId == userId).ToListAsync();
        }

        public async Task<WorkHours> AddWorkHoursAsync(WorkHours workHours)
        {
            _context.WorkHours.Add(workHours);
            await _context.SaveChangesAsync();
            return workHours;
        }

        public async Task<bool> DeleteWorkHoursAsync(int id)
        {
            var workHours = await _context.WorkHours.FindAsync(id);
            if (workHours == null) return false;

            _context.WorkHours.Remove(workHours);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
