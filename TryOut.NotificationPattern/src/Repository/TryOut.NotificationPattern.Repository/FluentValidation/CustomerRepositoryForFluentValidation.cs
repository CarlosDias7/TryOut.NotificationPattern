using TryOut.NotificationPattern.Domain.FluentValidation;
using TryOut.NotificationPattern.Repository.Abstractions;

namespace TryOut.NotificationPattern.Repository.FluentValidation
{
    public class CustomerRepositoryForFluentValidation : RepositoryForFluentValidation<Customer, CustomerValidator>, ICustomerRepository
    {
    }
}