using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Domain.Events;

public record BadgeCreatedEvent(Badge badge) : BaseEvent, IImmediateEvent;