using Microsoft.Extensions.DependencyInjection;
using TryOut.NotificationPattern.Domain.FluentValidation;
using TryOut.NotificationPattern.Repository.FluentValidation;

namespace TryOut.NotificationPattern.Repository.Register
{
    public static class RepositoryRegistration
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepositoryForFluentValidation>();
        }
    }
}