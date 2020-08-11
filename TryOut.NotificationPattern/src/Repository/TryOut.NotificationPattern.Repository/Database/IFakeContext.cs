using System.Collections.Generic;

namespace TryOut.NotificationPattern.Repository.Database
{
    public interface IFakeContext
    {
        List<TEntity> SetEntity<TEntity>() where TEntity : class;
    }
}