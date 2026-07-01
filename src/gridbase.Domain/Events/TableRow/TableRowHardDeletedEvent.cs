using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Domain.Events;

public record TableRowHardDeletedEvent(TableRow row) : BaseEvent, IImmediateEvent;



