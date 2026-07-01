using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Domain.Events;
// event
public record MenuItemDeletedEvent(MenuItem item) : BaseEvent, IImmediateEvent;
