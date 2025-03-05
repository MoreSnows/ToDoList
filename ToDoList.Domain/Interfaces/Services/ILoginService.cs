using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.Model;

namespace ToDoList.Domain.Interfaces.Services
{
    public interface ILoginService
    {
        Task RegisterUser(LoginModel login);
        Task<bool> Authenticate(LoginRequest login);
    }
}
