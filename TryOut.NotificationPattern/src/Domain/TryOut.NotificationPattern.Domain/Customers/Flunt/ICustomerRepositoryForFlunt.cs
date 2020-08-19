using System.Threading.Tasks;

namespace TryOut.NotificationPattern.Domain.Customers.Flunt
{
    public interface ICustomerRepositoryForFlunt
    {
        Task<bool> AnyAsync(long id);

        Task<bool> DeleteAsync(CustomerForFlunt entity);

        Task<CustomerForFlunt> GetAsync(long id);

        Task<bool> SaveAsync(CustomerForFlunt entity);
    }
}