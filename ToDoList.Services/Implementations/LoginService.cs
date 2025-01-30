using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Enums;
using ToDoList.Domain.Interfaces.Repositories;
using ToDoList.Domain.Interfaces.Services;
using ToDoList.Domain.ValueObjects;

namespace ToDoList.Services.Implementations
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly IActivityLogService _activityLogService;
        private readonly IPasswordService _passwordService;

        public LoginService(IUserRepository userRepository,
                            IActivityLogService activityLogService,
                            IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _activityLogService = activityLogService;
            _passwordService = passwordService;
        }

        public async Task<bool> Authenticate(LoginDTO login)
        {
            if (login == null)
            {
                throw new ArgumentNullException(nameof(login));
            }

            var user = await _userRepository.GetUser(new UserSearchCriteria
            {
                Email = login.Email,
                Username = login.Username,
                Phone = login.Phone
            });

            if(user == null)
            {
                return false;
            }

            if(await _passwordService.VerifyPassword(login.Password, user.PasswordHash))
            {
                await _activityLogService.AddActivityLog(user.Id, ActivityEnum.Login.Name, null);

                return true;
            }
            else
            {
                await _activityLogService.AddActivityLog(user.Id, ActivityEnum.LoginAttempt.Name, null);

                return false;
            }
        }

        public async Task RegisterUser(LoginDTO login)
        {
            if(login == null)
            {
                throw new ArgumentNullException(nameof(login));
            }

            var passwordHash = await _passwordService.HashPassword(login.Password);

            var user = new User
            {
                Email = login.Email,
                Username = login.Username,
                Phone = login.Phone,
                PasswordHash = passwordHash,
            };

            await _userRepository.Insert(user);

            await _activityLogService.AddActivityLog(user.Id, ActivityEnum.Register.Name, null);
        }
    }
}
