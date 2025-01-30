using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        Task<string> GenerateJwtToken(string email);
    }
}
