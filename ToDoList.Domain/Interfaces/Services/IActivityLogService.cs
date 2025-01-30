using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.Entities;

namespace ToDoList.Domain.Interfaces.Services
{
    public interface IActivityLogService
    {
        Task AddActivityLog(Guid userId, string action, string details);
        Task<IEnumerable<ActivityLog>> GetActivityLogsByUserId(Guid userId);
    }
}
