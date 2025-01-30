using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Domain.ValueObjects
{
    public class UserSearchCriteria
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }

        public UserSearchCriteria(string email = null, string username = null, string phone = null)
        {
            Email = email;
            Username = username;
            Phone = phone;
        }
    }
}
