using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Domain.Events;

public record TableColumnWithFunctionUpdatedEvent(TableColumn column) : BaseEvent, IImmediateEvent;