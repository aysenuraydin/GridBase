using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Domain.Events.DatatableEvents;

public record DatatableDeletedEvent(Datatable table) : BaseEvent, IImmediateEvent;