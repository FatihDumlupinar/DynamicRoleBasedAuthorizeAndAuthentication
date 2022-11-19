using DynamicyRoles.ApplicationService.Interfaces;
using DynamicyRoles.Persistence.Context;
using DynamicyRoles.Persistence.Repository;
using DynamicyRoles.Persistence.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicyRoles.Persistence.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration?.GetConnectionString("SQLConnection")));

            serviceCollection.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));
            serviceCollection.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
