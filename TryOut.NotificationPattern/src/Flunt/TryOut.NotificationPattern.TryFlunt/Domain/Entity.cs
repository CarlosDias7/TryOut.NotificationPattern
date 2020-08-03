using Flunt.Notifications;

namespace TryOut.NotificationPattern.TryFlunt.Domain
{
    public abstract class Entity<TId> : Notifiable
    {
        public Entity(TId id)
        {
            SetId(id);
        }

        public TId Id { get; private set; }

        public void SetId(TId id)
        {
            Id = id;
        }
    }
}