using FluentValidation;
using System;
using System.Text.RegularExpressions;

namespace TryOut.NotificationPattern.Domain.FluentValidation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        private const int MajorityAge = 18;

        public CustomerValidator()
        {
            RuleFor(x => x.Active)
                .Must((x, y) => !x.Admin)
                .When(x => !x.Active)
                .WithMessage("Can't set Active.");

            RuleFor(x => x.Birth)
                .NotEmpty()
                .WithMessage("Can't set Birth.")
                .LessThan(DateTime.Now)
                .WithMessage("Can't set Birth.")
                .Must(CustomerMustBeMajor)
                .WithMessage("Can't set Birth.");

            RuleFor(x => x.Document)
                .NotEmpty()
                .WithMessage("Can't set Document.")
                .Must(ValidateDocumentFormat)
                .WithMessage("Can't set Document.");

            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Can't set Id.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Can't set Name.")
                .MaximumLength(Customer.NameMaxLength)
                .WithMessage("Can't set Name.");
        }

        private bool CustomerMustBeMajor(DateTime birth)
        {
            TimeSpan diffSpan = DateTime.Now - birth;
            var age = (DateTime.MinValue + diffSpan).Year - 1;
            return age >= MajorityAge;
        }

        private bool ValidateDocumentFormat(string document)
        {
            var regex = new Regex($@"[0-9]{Customer.DocumentMaxLength}$");
            return regex.IsMatch(document);
        }
    }
}