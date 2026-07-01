using gridbase.Domain.Common;
namespace gridbase.Domain.Entities;

public class Badge : BaseAuditableEntity<long>
{
    public string Name { get; set; } = null!;
    public string Color { get; set; } = null!;

    public long? MenuItemId { get; set; }
    public MenuItem? MenuItemFk { get; set; }
}