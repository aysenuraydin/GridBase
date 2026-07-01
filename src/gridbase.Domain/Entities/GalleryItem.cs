using gridbase.Domain.Common;

namespace gridbase.Domain.Entities;

public class GalleryItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}