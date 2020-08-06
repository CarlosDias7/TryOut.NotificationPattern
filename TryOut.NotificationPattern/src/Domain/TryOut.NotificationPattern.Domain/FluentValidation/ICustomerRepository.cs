using System.Threading.Tasks;

namespace TryOut.NotificationPattern.Domain.FluentValidation
{
    public interface ICustomerRepository
    {
        Task<bool> DeleteAsync(Customer entity);

        Task<bool> SaveAsync(Customer entity);
    }
}