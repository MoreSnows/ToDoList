using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Domain.Interfaces.Services;
using ToDoList.Services.Implementations;

namespace ToDoList.Services.Configurations
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection ServiceConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IActivityLogService, ActivityLogService>();
            services.AddSingleton<ILoginService, LoginService>();
            services.AddSingleton<IPasswordService, PasswordService>();
            services.AddSingleton<ITokenService, TokenService>();

            return services;
        }
    }
}
