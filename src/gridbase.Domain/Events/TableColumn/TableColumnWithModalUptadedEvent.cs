using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Domain.Events;

public record TableColumnWithModalUpdatedEvent(TableColumn column) : BaseEvent, IImmediateEvent;