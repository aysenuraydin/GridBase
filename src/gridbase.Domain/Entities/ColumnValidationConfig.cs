using gridbase.Domain.Common;
using gridbase.Domain.Enums;
namespace gridbase.Domain.Entities;

public class ColumnValidationConfig : BaseAuditableEntity<long>
{
    public long ColumnId { get; set; }
    public FieldTypeEnum Type { get; set; }
    public List<RulesValidationConfig> Rules { get; set; } = new();
};