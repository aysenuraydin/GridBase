using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Domain.Events.DatatableEvents;

public record DatatableHardDeletedEvent(Datatable table) : BaseEvent, IImmediateEvent;