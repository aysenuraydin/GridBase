using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Domain.Events.DatatableEvents;

public record DatatableCreatedEvent(Datatable table) : BaseEvent, IImmediateEvent;