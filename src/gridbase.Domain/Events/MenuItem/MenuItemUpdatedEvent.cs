using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Domain.Events;

public record MenuItemUpdatedEvent(MenuItem item) : BaseEvent, IImmediateEvent;