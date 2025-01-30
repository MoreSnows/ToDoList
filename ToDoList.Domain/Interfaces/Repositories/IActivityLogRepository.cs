using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.Entities;

namespace ToDoList.Domain.Interfaces.Repositories
{
    public interface IActivityLogRepository : IRepositoryBase<ActivityLog>
    {
        Task Add(ActivityLog log);
        Task<IEnumerable<ActivityLog>> GetByUserId(Guid userId);
    }
}
