using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Domain.Events;

public record TableColumnRestoredDeletedEvent(TableColumn column) : BaseEvent, IImmediateEvent;