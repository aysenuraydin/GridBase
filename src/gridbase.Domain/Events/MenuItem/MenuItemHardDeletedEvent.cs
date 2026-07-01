using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Domain.Events;

public record MenuItemHardDeletedEvent(MenuItem item) : BaseEvent, IImmediateEvent;