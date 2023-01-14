using Microsoft.Extensions.DependencyInjection;
using Ruper.DAL.Repositories.Contracts;
using Ruper.DAL.Repositories;

namespace Ruper.DAL
{
    public static class DataAccessLayerServiceRegistration
    {
        public static IServiceCollection AddDalServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EfCoreRepository<>));

            return services;
        }
    }
}
