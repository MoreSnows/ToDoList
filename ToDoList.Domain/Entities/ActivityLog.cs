using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Domain.Entities
{
    [Table("ActivityLogs")]
    public class ActivityLog
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; } 
        public DateTimeOffset ExecutedAt { get; set; } = DateTimeOffset.UtcNow;
        public string Action { get; set; }
        public string Details { get; set; }
    }
}
