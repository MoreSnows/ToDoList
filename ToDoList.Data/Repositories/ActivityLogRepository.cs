using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Interfaces.Repositories;

namespace ToDoList.Data.Repositories
{
    public class ActivityLogRepository : RepositoryBase<ActivityLog>, IActivityLogRepository
    {
        public ActivityLogRepository(Context context) : base(context)
        {
        }

        public async Task Add(ActivityLog log)
        {
            await InsertAsync<Guid>(log);
        }

        public Task<IEnumerable<ActivityLog>> GetByUserId(Guid userId)
        {
            var query = @"SELECT * FROM ActivityLog
                          WHERE UserId = @userId";

            return QueryAsync<ActivityLog>(query, userId);
        }
    }
}
