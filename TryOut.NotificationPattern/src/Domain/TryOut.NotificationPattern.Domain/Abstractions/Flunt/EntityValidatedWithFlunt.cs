using Flunt.Notifications;

namespace TryOut.NotificationPattern.Domain.Abstractions.Flunt
{
    public abstract class EntityValidatedWithFlunt<TId> : Notifiable, IEntityValidatedWithFlunt
        where TId : struct
    {
        public EntityValidatedWithFlunt(TId id)
        {
            SetId(id);
        }

        public TId Id { get; protected set; }

        protected abstract void SetId(TId id);
    }
}