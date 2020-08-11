using Microsoft.Extensions.DependencyInjection;
using TryOut.NotificationPattern.Domain.Customers.FluentValidation;
using TryOut.NotificationPattern.Repository.Database;
using TryOut.NotificationPattern.Repository.FluentValidation;

namespace TryOut.NotificationPattern.Repository.Register
{
    public static class RepositoryRegistration
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepositoryForFluentValidation, CustomerRepositoryForFluentValidation>();
            services.AddSingleton<IFakeContext, FakeContext>();
        }
    }
}