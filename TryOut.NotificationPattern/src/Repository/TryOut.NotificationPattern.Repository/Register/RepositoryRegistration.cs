using Microsoft.Extensions.DependencyInjection;
using TryOut.NotificationPattern.Domain.Customers.FluentValidation;
using TryOut.NotificationPattern.Domain.Customers.Flunt;
using TryOut.NotificationPattern.Repository.Customers.FluentValidation;
using TryOut.NotificationPattern.Repository.Customers.Flunt;
using TryOut.NotificationPattern.Repository.Database;

namespace TryOut.NotificationPattern.Repository.Register
{
    public static class RepositoryRegistration
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepositoryForFluentValidation, CustomerRepositoryForFluentValidation>();
            services.AddScoped<ICustomerRepositoryForFlunt, CustomerRepositoryForFlunt>();
            services.AddSingleton<IFakeContext, FakeContext>();
        }
    }
}