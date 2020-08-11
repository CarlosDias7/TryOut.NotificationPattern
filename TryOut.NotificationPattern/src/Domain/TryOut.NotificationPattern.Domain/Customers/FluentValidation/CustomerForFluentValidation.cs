using System;
using TryOut.NotificationPattern.Domain.Abstractions.FluentValidation;

namespace TryOut.NotificationPattern.Domain.Customers.FluentValidation
{
    public class CustomerForFluentValidation : EntityValidatedWithFluentValidation<long, CustomerForFluentValidation, CustomerValidator>
    {
        public const short DocumentMaxLength = 11;
        public const short NameMaxLength = 60;

        public CustomerForFluentValidation(long id, DateTime birth, string document, string name, decimal initialCredits)
            : base(id)
        {
            Active = true;
            Birth = birth;
            Document = document;
            Name = name;
            Credits = initialCredits;
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

        public void DecressCredits(decimal value)
        {
            // TO DO
            // Put a rule to validate value parameter. It can't be less than 0.

            Credits -= value;
        }

        public override bool Equals(object obj)
        {
            if (obj is CustomerForFluentValidation entityToCompare)
                return entityToCompare.Id == Id;

            return false;
        }

        public override int GetHashCode() => Id.GetHashCode();

        public void SetActive(bool active)
        {
            // TO DO
            // Put a rule to validate the action. It will be blocked if Admin is true.

            Active = active;
        }

        public void SetAdmin(bool admin)
        {
            // TO DO
            // Put a rule to validate the action. It will be blocked if Active is false.

            Admin = admin;
        }

        public void SetBirth(DateTime birth) => Birth = birth;

        public void SetName(string name) => Name = name;

        public override string ToString()
            => $@"Customer validated with Fluent Validation.
                    Id: {Id}
                    Name: {Name}
                    Birth: {Birth}
                    Document: {Document}
                    {(Admin ? "This Customer is a Admin." : string.Empty)}";
    }
}