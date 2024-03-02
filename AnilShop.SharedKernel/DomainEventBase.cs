using MediatR;

namespace AnilShop.SharedKernel;

public abstract class DomainEventBase : INotification
{
    public DateTime DateOccured { get; protected set; } = DateTime.UtcNow;
}