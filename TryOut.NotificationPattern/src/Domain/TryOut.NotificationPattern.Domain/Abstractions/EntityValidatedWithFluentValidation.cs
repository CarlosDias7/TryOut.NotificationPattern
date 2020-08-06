using FluentValidation;
using FluentValidation.Results;

namespace TryOut.NotificationPattern.Domain.Abstractions
{
    public abstract class EntityValidatedWithFluentValidation<TId, TEntity, TValidator> : IEntityValidatedWithFluentValidation<TValidator>
        where TId : struct
        where TEntity : EntityValidatedWithFluentValidation<TId, TEntity, TValidator>
        where TValidator : AbstractValidator<TEntity>
    {
        public EntityValidatedWithFluentValidation(TId id)
        {
            Id = id;
        }

        public TId Id { get; private set; }
        public bool Valid => ValidationResult?.IsValid ?? false;
        public ValidationResult ValidationResult { get; private set; }

        public virtual void Validate(TValidator validator)
        {
            ValidationResult = validator.Validate((TEntity)this);
        }
    }
}