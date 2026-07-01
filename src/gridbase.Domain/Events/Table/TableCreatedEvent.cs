using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Domain.Events;

public record TableCreatedEvent(Datatable table) : BaseEvent, IImmediateEvent;