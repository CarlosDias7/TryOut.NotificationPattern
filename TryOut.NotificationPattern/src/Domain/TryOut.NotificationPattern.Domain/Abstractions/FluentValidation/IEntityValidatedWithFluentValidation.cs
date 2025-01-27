﻿using FluentValidation;

namespace TryOut.NotificationPattern.Domain.Abstractions.FluentValidation
{
    public interface IEntityValidatedWithFluentValidation<TValidator>
        where TValidator : IValidator
    {
        bool Valid { get; }

        void Validate(TValidator validator);
    }
}