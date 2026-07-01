using gridbase.Domain.Common;
using gridbase.Domain.Enums;
namespace gridbase.Domain.Entities;

public class ColumnUIConfig : BaseAuditableEntity<long>
{
    public long ColumnId { get; set; }
    public string? Value { get; set; } = null!;
    public AttributeEnum Type { get; set; }
};