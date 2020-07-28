using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Teleport.Data.Context;
using Teleport.Data.Entity;
using Teleport.Data.Repository;
using Teleport.Service.Interfaces;
using Teleport.Service.Services;

namespace Teleport.Service.ServiceCollection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCommonInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddScoped<IDataContext, TeleportDbContext>();

            //Repositories
            serviceCollection.AddScoped<IRepository<User>, Repository<User>>();

            //services
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IDistanceService, DistanceService>();
        }
    }
}
