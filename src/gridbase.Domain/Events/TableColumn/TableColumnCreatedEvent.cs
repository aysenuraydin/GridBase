using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Domain.Events;

public record TableColumnCreatedEvent(TableColumn column) : BaseEvent, IImmediateEvent;


