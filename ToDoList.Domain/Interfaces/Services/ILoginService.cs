using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DTOs;

namespace ToDoList.Domain.Interfaces.Services
{
    public interface ILoginService
    {
        Task RegisterUser(LoginDTO login);
        Task<bool> Authenticate(LoginDTO login);
    }
}
