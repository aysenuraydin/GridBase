using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Domain.Events;

public record TableColumnWithDesignUpdatedEvent(TableColumn column) : BaseEvent, IImmediateEvent;