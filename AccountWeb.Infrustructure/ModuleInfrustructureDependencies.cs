using AccountWeb.Infrustructure.Abstracts;
using AccountWeb.Infrustructure.InfrustructureBases;
using AccountWeb.Infrustructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AccountWeb.Infrustructure
{
    public static class ModuleInfrustructureDependencies
    {
        public static IServiceCollection AddInfrustructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            return services;
        }
    }
}
