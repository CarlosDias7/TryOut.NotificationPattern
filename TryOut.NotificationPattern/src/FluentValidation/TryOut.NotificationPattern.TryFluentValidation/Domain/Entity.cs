using FluentValidation;
using FluentValidation.Results;

namespace TryOut.NotificationPattern.TryFluentValidation.Domain
{
    public abstract class Entity<TId, TEntity, TValidator>
        where TId : struct
        where TEntity : Entity<TId, TEntity, TValidator>
        where TValidator : AbstractValidator<TEntity>
    {
        public Entity(TId id)
        {
            Id = id;
        }

        public TId Id { get; private set; }
        public bool IsValid => ValidationResult?.IsValid ?? false;
        public ValidationResult ValidationResult { get; private set; }

        public virtual void Validate(TValidator validator)
        {
            ValidationResult = validator.Validate((TEntity)this);
        }
    }
}