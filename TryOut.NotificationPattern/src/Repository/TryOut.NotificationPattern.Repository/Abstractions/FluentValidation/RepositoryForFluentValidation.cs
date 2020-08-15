using FluentValidation;
using System;
using System.Linq;
using System.Threading.Tasks;
using TryOut.NotificationPattern.Domain.Abstractions.FluentValidation;
using TryOut.NotificationPattern.Repository.Database;

namespace TryOut.NotificationPattern.Repository.Abstractions.FluentValidation
{
    public abstract class RepositoryForFluentValidation<TEntity, TValidator>
        where TEntity : class, IEntityValidatedWithFluentValidation<TValidator>
        where TValidator : AbstractValidator<TEntity>, new()
    {
        private readonly IFakeContext _fakeContext;

        public RepositoryForFluentValidation(IFakeContext fakeContext)
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
            entity.Validate(new TValidator());
            if (!entity.Valid) return false;

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