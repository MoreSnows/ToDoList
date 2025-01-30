using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Domain.Entities
{
    public class ActivityLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; } 
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
        public string Action { get; set; }
        public string Details { get; set; }
    }
}
