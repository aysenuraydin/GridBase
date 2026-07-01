using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Domain.Events.DatatableEvents;

public record DatatableRestoreDeletedEvent(Datatable table) : BaseEvent, IImmediateEvent;