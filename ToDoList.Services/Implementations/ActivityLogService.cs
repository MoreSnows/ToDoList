using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Interfaces.Repositories;
using ToDoList.Domain.Interfaces.Services;

namespace ToDoList.Services.Implementations
{
    public class ActivityLogService : IActivityLogService
    {
        private readonly IActivityLogRepository _activityLogRepository;

        public ActivityLogService(IActivityLogRepository activityLogRepository)
        {
            _activityLogRepository = activityLogRepository;
        }

        public async Task AddActivityLog(Guid userId, string action, string details)
        {
            if (userId == Guid.Empty) throw new ArgumentException("User ID cannot be empty", nameof(userId));
            if (string.IsNullOrEmpty(action)) throw new ArgumentException("Action cannot be null or empty", nameof(action));

            var log = new ActivityLog
            {
                UserId = userId,
                Action = action,
                Details = details
            };

            await _activityLogRepository.Add(log);
        }

        public async Task<IEnumerable<ActivityLog>> GetActivityLogsByUserId(Guid userId)
        {
            if (userId == Guid.Empty) throw new ArgumentException("User ID cannot be empty", nameof(userId));

            return await _activityLogRepository.GetByUserId(userId);
        }
    }
}
