using FluentValidation;
using System.Collections.Generic;
using System.Threading.Tasks;
using TryOut.NotificationPattern.Domain.Abstractions;

namespace TryOut.NotificationPattern.Repository.Abstractions
{
    public abstract class RepositoryForFluentValidation<TEntity, TValidator>
        where TEntity : IEntityValidatedWithFluentValidation<TValidator>
        where TValidator : AbstractValidator<TEntity>, new()
    {
        private readonly List<TEntity> _fakeDbSet;

        public RepositoryForFluentValidation()
        {
            _fakeDbSet = new List<TEntity>();
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            if (entity is null) return false;
            _fakeDbSet.Remove(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> SaveAsync(TEntity entity)
        {
            if (entity is null) return false;
            entity.Validate(new TValidator());
            if (!entity.Valid) return false;

            _fakeDbSet.Remove(entity);
            _fakeDbSet.Add(entity);
            return await Task.FromResult(true);
        }
    }
}