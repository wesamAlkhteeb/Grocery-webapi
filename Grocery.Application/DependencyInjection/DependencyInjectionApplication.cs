using Grocery.Application.Services;
using Grocery.Application.Services.interfaces;
using Microsoft.Extensions.DependencyInjection;


namespace Grocery.Application.DependencyInjection
{
    public static class DependencyInjectionApplication
    {
        public static IServiceCollection DIApplication(this IServiceCollection service) {
            service.AddScoped<IAccountService, AccountService>();
            return service;
        }
    }
}
