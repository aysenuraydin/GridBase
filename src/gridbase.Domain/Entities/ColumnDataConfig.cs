using gridbase.Domain.Common;
using gridbase.Domain.Enums;

namespace gridbase.Domain.Entities;

public class ColumnDataConfig : BaseAuditableEntity<long>
{
    public long ColumnId { get; set; }
    public string? Value { get; set; } = null!;
    public PropertyEnum Type { get; set; }
};