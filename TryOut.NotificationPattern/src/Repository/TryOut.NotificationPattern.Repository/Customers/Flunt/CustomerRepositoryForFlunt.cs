using System.Threading.Tasks;
using TryOut.NotificationPattern.Domain.Customers.Flunt;
using TryOut.NotificationPattern.Repository.Abstractions.Flunt;
using TryOut.NotificationPattern.Repository.Database;

namespace TryOut.NotificationPattern.Repository.Customers.Flunt
{
    public class CustomerRepositoryForFlunt : RepositoryForFlunt<CustomerForFlunt>, ICustomerRepositoryForFlunt
    {
        public CustomerRepositoryForFlunt(IFakeContext fakeContext)
            : base(fakeContext)
        {
        }

        public async Task<CustomerForFlunt> GetAsync(long id) => await GetAsync(x => x.Id == id);
    }
}