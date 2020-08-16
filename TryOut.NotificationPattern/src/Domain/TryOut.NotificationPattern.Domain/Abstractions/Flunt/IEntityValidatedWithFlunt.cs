namespace TryOut.NotificationPattern.Domain.Abstractions.Flunt
{
    public interface IEntityValidatedWithFlunt
    {
        bool Invalid { get; }
        bool Valid { get; }
    }
}