using System;
using System.Collections.Generic;
using TryOut.NotificationPattern.Domain.Customers.FluentValidation;
using TryOut.NotificationPattern.Domain.Customers.Flunt;

namespace TryOut.NotificationPattern.Repository.Database
{
    internal class FakeContext : IFakeContext
    {
        private readonly List<CustomerForFluentValidation> _dbSetCustomersForFluentValidation;
        private readonly List<CustomerForFlunt> _dbSetCustomersForFlunt;

        public FakeContext()
        {
            _dbSetCustomersForFluentValidation = new List<CustomerForFluentValidation>();
            _dbSetCustomersForFlunt = new List<CustomerForFlunt>();
        }

        /// <summary>
        /// Just a method to simulate a choose of a real Dbset.
        /// </summary>
        public List<TEntity> SetEntity<TEntity>()
            where TEntity : class
        {
            if (typeof(TEntity) == typeof(CustomerForFluentValidation))
                return _dbSetCustomersForFluentValidation as List<TEntity>;
            throw new ArgumentException("The specified type is invalid.");
        }
    }
}