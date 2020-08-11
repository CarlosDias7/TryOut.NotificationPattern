using Flunt.Validations;
using System;
using TryOut.NotificationPattern.Domain.Abstractions.Flunt;

namespace TryOut.NotificationPattern.Domain.Customers.Flunt
{
    public class CustomerForFlunt : EntityValidatedWithFlunt<long>
    {
        public const short DocumentMaxLength = 11;
        public const short NameMaxLength = 60;
        private const int MajorityAge = 18;

        public CustomerForFlunt(long id, DateTime birth, string document, string name, decimal initialCredits)
            : base(id)
        {
            SetActive(true);
            SetBirth(birth);
            SetDocument(document);
            SetName(name);
            SetInitialCredits(initialCredits);
        }

        public bool Active { get; private set; }
        public bool Admin { get; private set; }
        public DateTime Birth { get; private set; }
        public decimal Credits { get; private set; }
        public string Document { get; private set; }
        public string Name { get; private set; }

        public override bool Equals(object obj)
        {
            if (obj is CustomerForFlunt entityToCompare)
                return entityToCompare.Id == Id;

            return false;
        }

        public override int GetHashCode() => Id.GetHashCode();

        public void SetActive(bool active)
        {
            if (!active && Admin)
            {
                AddNotification(nameof(Active), "You can't inactive a Admin Customer.");
                return;
            }

            Active = active;
        }

        public void SetAdmin(bool admin)
        {
            if (admin && !Active)
            {
                AddNotification(nameof(Active), "You can't set a Customer inactive as a Admin.");
                return;
            }

            Active = admin;
        }

        public void SetBirth(DateTime birth)
        {
            AddNotifications(new Contract()
                .IsLowerOrEqualsThan(birth, DateTime.MinValue, nameof(Birth), "Birth must be informed."));

            if (!CustomerMustBeMajor(birth))
                AddNotification(nameof(Birth), $"Customer must be {MajorityAge} years old or older.");

            if (Valid) Birth = birth;
        }

        public void SetName(string name)
        {
            AddNotifications(new Contract()
                .IsNullOrEmpty(name, nameof(Name), "Name must be informed.")
                .HasMaxLen(name, NameMaxLength, nameof(Name), $"The name must have until {NameMaxLength} characters "));
        }

        protected override void SetId(long id)
        {
            AddNotifications(new Contract()
                .IsGreaterThan(Id, default, nameof(Id), "Id has been defined.")
                .IsLowerOrEqualsThan(id, default, nameof(Id), "Id must be informed."));

            if (Valid) Id = id;
        }

        private bool CustomerMustBeMajor(DateTime birth)
        {
            TimeSpan diffSpan = DateTime.Now - birth;
            var age = (DateTime.MinValue.Add(diffSpan)).Year - 1;
            return age >= MajorityAge;
        }

        private void SetDocument(string document)
        {
            AddNotifications(new Contract()
                .IsNullOrEmpty(document, nameof(Document), "Document must be informed.")
                .Matchs(document, "[0-9]{" + DocumentMaxLength + "}$", nameof(Document), "Document informed is invalid."));

            if (Valid) Document = document;
        }

        private void SetInitialCredits(decimal initialCredits)
        {
            AddNotifications(new Contract()
                .IsLowerThan(initialCredits, default, nameof(Credits), "The initial credits can't be lower than 0 (zero)."));

            if (Valid) Credits = initialCredits;
        }
    }
}