using System.Threading.Tasks;
using TryOut.NotificationPattern.Domain.Customers.FluentValidation;
using TryOut.NotificationPattern.Repository.Abstractions.FluentValidation;
using TryOut.NotificationPattern.Repository.Database;

namespace TryOut.NotificationPattern.Repository.Customers.FluentValidation
{
    public class CustomerRepositoryForFluentValidation : RepositoryForFluentValidation<CustomerForFluentValidation, CustomerValidator>, ICustomerRepositoryForFluentValidation
    {
        public CustomerRepositoryForFluentValidation(IFakeContext fakeContext)
            : base(fakeContext)
        {
        }

        public async Task<CustomerForFluentValidation> GetAsync(long id) => await GetAsync(x => x.Id == id);
    }
}