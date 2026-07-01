using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Domain.Events;

public record TableColumnUpdatedEvent(TableColumn column) : BaseEvent, IImmediateEvent;