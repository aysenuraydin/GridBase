using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Domain.Events;

public record TableRowCreatedEvent(TableRow row) : BaseEvent, IImmediateEvent;






