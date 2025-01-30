using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Domain.Enums
{
    public class ActivityEnum : Enumeration
    {
        public static readonly ActivityEnum Register = new ActivityEnum(1, "Register");
        public static readonly ActivityEnum Login = new ActivityEnum(2, "Login");
        public static readonly ActivityEnum LoginAttempt = new ActivityEnum(3, "Login Attempt");

        public ActivityEnum(int id, string name) : base(id, name)
        {
        }
    }
}
