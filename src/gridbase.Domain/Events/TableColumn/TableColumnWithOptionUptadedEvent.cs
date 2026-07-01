using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Domain.Events;

public record TableColumnWithOptionUpdatedEvent(TableColumn column) : BaseEvent, IImmediateEvent;