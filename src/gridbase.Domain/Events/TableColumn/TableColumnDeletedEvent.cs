using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Domain.Events;

public record TableColumnDeletedEvent(TableColumn column) : BaseEvent, IImmediateEvent;