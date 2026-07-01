using gridbase.Domain.Common;

namespace gridbase.Domain.Entities;

public class Document
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
}

