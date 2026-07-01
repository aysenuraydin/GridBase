using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Domain.Events;

public record TableCellCreatedEvent(TableCell cell) : BaseEvent, IImmediateEvent;