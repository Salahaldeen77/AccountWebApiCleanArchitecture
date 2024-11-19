using AccountWeb.Data.Entities;
using AccountWeb.Infrustructure.Abstracts;
using AccountWeb.Infrustructure.Repositories;
using AccountWeb.Service.Abstracts;
using AccountWeb.Service.Implementations;
using LedgerEntryWeb.Service.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace AccountWeb.Service
{
    public static class ModuleServicesDependencies
    {
        public static IServiceCollection AddServicesDependencies(this IServiceCollection services)
        {
            services.AddTransient<IAccountService, AccountService>().
                    AddTransient<ITransactionService, TransactionService>()
                    .AddTransient<ITransactionAccountService, TransactionAccountService>()
                    .AddTransient<ILedgerEntriesService, LedgerEntriesService>();
            return services;
        }

    }
}
