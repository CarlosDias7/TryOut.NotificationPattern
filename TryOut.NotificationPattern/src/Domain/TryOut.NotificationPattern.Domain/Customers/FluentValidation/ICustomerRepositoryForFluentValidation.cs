using System.Threading.Tasks;

namespace TryOut.NotificationPattern.Domain.Customers.FluentValidation
{
    public interface ICustomerRepositoryForFluentValidation
    {
        Task<bool> DeleteAsync(CustomerForFluentValidation entity);

        Task<CustomerForFluentValidation> GetAsync(long id);

        Task<bool> SaveAsync(CustomerForFluentValidation entity);
    }
}