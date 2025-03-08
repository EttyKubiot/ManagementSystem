using ManagementSystem.Data.Repositories;
using ManagementSystem.Data.Entities;
using ManagementSystem.Data;

namespace ManagementSystem.Services
{
    public class WorkHoursService
    {
        private readonly WorkHoursRepository _workHoursRepository;

        public WorkHoursService(WorkHoursRepository workHoursRepository)
        {
            _workHoursRepository = workHoursRepository;
        }

        public async Task<List<WorkHours>> GetWorkHoursByUserIdAsync(int userId)
        {
            return await _workHoursRepository.GetWorkHoursByUserIdAsync(userId);
        }

        public async Task<WorkHours> AddWorkHoursAsync(WorkHours workHours)
        {
            return await _workHoursRepository.AddWorkHoursAsync(workHours);
        }

        public async Task<bool> DeleteWorkHoursAsync(int id)
        {
            return await _workHoursRepository.DeleteWorkHoursAsync(id);
        }
    }
}
