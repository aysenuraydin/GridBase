using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Domain.Events.DatatableEvents;

public record ForeignTableUpdatedEvent(Datatable table) : BaseEvent, IImmediateEvent;