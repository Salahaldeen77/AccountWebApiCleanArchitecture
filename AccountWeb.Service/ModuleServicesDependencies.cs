using AccountWeb.Service.Abstracts;
using AccountWeb.Service.AuthServices.Implementations;
using AccountWeb.Service.AuthServices.Interfaces;
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
                    .AddTransient<ILedgerEntriesService, LedgerEntriesService>()
                    .AddTransient<IAuthenticationService, AuthenticationService>()
                    .AddTransient<IAuthorizationService, AuthorizationService>()
                    .AddTransient<IEmailsService, EmailsService>()
                    .AddTransient<IUserService, UserService>()
                    .AddTransient<ICurrentUserService, CurrentUserService>();
            return services;
        }

    }
}
