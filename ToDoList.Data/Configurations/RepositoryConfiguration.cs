using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Data.Repositories;
using ToDoList.Domain.Interfaces.Repositories;

namespace ToDoList.Data.Configurations
{
    public static class RepositoryConfiguration
    {
        public static IServiceCollection RepositoryConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(provider => new SqlConnection(configuration.GetConnectionString("Default")));

            services.AddSingleton<Context>();

            services.AddSingleton<IActivityLogRepository, ActivityLogRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();

            return services;
        }
    }
}
