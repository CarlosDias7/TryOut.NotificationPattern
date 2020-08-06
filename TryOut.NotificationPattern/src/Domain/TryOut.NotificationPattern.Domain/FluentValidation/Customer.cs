using System;
using TryOut.NotificationPattern.Domain.Abstractions;

namespace TryOut.NotificationPattern.Domain.FluentValidation
{
    public class Customer : EntityValidatedWithFluentValidation<long, Customer, CustomerValidator>
    {
        public const short DocumentMaxLength = 11;
        public const short NameMaxLength = 60;

        public Customer(long id, DateTime birth, string document, string name, decimal initialCredits)
            : base(id)
        {
            Active = true;
            Birth = birth;
            Document = document;
            Name = name;
            Credits = initialCredits;

            Validate(new CustomerValidator());
        }

        public bool Active { get; private set; }
        public bool Admin { get; private set; }
        public DateTime Birth { get; private set; }
        public decimal Credits { get; private set; }
        public string Document { get; private set; }
        public string Name { get; private set; }

        public void AddCredits(decimal value)
        {
            // TO DO
            // Put a rule to validate value parameter. It can't be less than 0.

            Credits += value;
        }

        public void ChangeToAdmin()
        {
            // TO DO
            // Put a rule to validate the action. It will be blocked if Active is false.

            Admin = true;
        }

        public void DecressCredits(decimal value)
        {
            // TO DO
            // Put a rule to validate value parameter. It can't be less than 0.

            Credits -= value;
        }

        public override bool Equals(object obj)
        {
            if (obj is Customer entityToCompare)
                return entityToCompare.Id == Id;

            return false;
        }

        public override int GetHashCode() => Id.GetHashCode();

        public void Inative()
        {
            // TO DO
            // Put a rule to validate the action. It will be blocked if Admin is true.

            Active = false;
        }
    }
}