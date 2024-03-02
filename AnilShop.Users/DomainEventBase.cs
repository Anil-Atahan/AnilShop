using MediatR;

namespace AnilShop.Users;

public abstract class DomainEventBase : INotification
{
    public DateTime DateOccured { get; protected set; } = DateTime.UtcNow;
}