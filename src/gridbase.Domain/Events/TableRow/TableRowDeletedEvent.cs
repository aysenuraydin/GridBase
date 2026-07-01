using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Domain.Events;

public record TableRowDeletedEvent(TableRow row) : BaseEvent, IImmediateEvent;

