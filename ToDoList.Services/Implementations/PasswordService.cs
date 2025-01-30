using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using ToDoList.Domain.Interfaces.Services;

namespace ToDoList.Services.Implementations
{
    public class PasswordService : IPasswordService
    {
        public async Task<string> HashPassword(string password)
        {
            return await Task.Run(() => BCrypt.Net.BCrypt.HashPassword(password));
        }

        public async Task<bool> VerifyPassword(string password, string hashedPassword)
        {
            return await Task.Run(() => BCrypt.Net.BCrypt.Verify(password, hashedPassword));
        }
    }
}
