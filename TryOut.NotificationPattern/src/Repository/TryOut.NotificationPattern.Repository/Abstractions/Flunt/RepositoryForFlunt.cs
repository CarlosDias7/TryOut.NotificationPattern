using System;
using System.Linq;
using System.Threading.Tasks;
using TryOut.NotificationPattern.Domain.Customers.Flunt;
using TryOut.NotificationPattern.Repository.Database;

namespace TryOut.NotificationPattern.Repository.Abstractions.Flunt
{
    public class RepositoryForFlunt<TEntity>
        where TEntity : class, ICustomerForFlunt
    {
        private readonly IFakeContext _fakeContext;

        public RepositoryForFlunt(IFakeContext fakeContext)
        {
            _fakeContext = fakeContext;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            if (entity is null) return false;
            _fakeContext.SetEntity<TEntity>().Remove(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> SaveAsync(TEntity entity)
        {
            if (entity is null) return false;
            if (entity.Invalid) return false;

            _fakeContext.SetEntity<TEntity>().Remove(entity);
            _fakeContext.SetEntity<TEntity>().Add(entity);
            return await Task.FromResult(true);
        }

        protected async Task<TEntity> GetAsync(Func<TEntity, bool> predicate)
        {
            var result = _fakeContext.SetEntity<TEntity>().FirstOrDefault(predicate);
            return await Task.FromResult(result);
        }
    }
}