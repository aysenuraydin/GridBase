using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Domain.Events;

public record TableRowRestoredDeletedEvent(TableRow row) : BaseEvent, IImmediateEvent;





