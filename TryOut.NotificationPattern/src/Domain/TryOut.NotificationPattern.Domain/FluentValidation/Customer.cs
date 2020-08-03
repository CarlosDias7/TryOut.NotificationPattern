using System;
using TryOut.NotificationPattern.TryFluentValidation.Domain;

namespace TryOut.NotificationPattern.Domain.FluentValidation
{
    public class Customer : Entity<long, Customer, CustomerValidator>
    {
        public const short DocumentMaxLength = 11;
        public const short NameMaxLength = 60;

        public Customer(long id, DateTime birth, string document, string name)
            : base(id)
        {
            Active = true;
            Birth = birth;
            Document = document;
            Name = name;

            Validate(new CustomerValidator());
        }

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

        public void AddCredits(decimal value) => Credits += value;

        public void DecressCredits(decimal value) => Credits -= value;

        public void Inative() => Active = false;
    }
}