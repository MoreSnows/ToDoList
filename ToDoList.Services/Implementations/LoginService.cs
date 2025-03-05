using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ToDoList.Domain.Model;
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

        public async Task<bool> Authenticate(LoginRequest login)
        {
            var loginDto = IdentifyType(login);

            if (loginDto == null)
            {
                throw new ArgumentNullException(nameof(loginDto));
            }

            var user = await _userRepository.GetUser(new UserSearchCriteria
            {
                Email = loginDto.Email,
                Username = loginDto.Username,
                Phone = loginDto.Phone
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

        public async Task RegisterUser(LoginModel login)
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

        public LoginModel IdentifyType(LoginRequest loginRequest)
        {
            var login = new LoginModel
            {
                Password = loginRequest.Password
            };

            if (IsPhoneNumber(loginRequest.Login))
            {
                login.Phone = loginRequest.Login;
                return login;
            }

            if (IsEmail(loginRequest.Login))
            {
                login.Email = loginRequest.Login;
                return login;
            }

            if (IsUsername(loginRequest.Login))
            {
                login.Username = loginRequest.Login;
                return login;
            }

            return null;
        }

        private bool IsPhoneNumber(string input)
        {
            string pattern = @"^(\+\d{1,3}\s?)?(\(\d{2,4}\)|\d{2,4})[\s-]?\d{4,5}[\s-]?\d{4}$";
            return Regex.IsMatch(input, pattern);
        }

        private bool IsEmail(string input)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(input, pattern);
        }

        private bool IsUsername(string input)
        {
            string pattern = @"^[a-zA-Z0-9_\.]{3,20}$";
            return Regex.IsMatch(input, pattern);
        }
    }

}
