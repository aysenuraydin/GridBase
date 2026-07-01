using MediatR;

namespace gridbase.Domain.Common;

public abstract record BaseEvent : INotification
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}