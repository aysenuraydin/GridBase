namespace gridbase.FileApi.Data;

public class FileEntity
{
    public long Id { get; set; }
    public string OriginalName { get; set; } = null!;
    public string LocalName { get; set; } = Guid.NewGuid().ToString();
    public string? ContentType { get; set; }
    public string? Extension { get; set; }
    public long Size { get; set; }
    public string? FilePath { get; set; }
    public DateTime CreatedAt { get; set; }

    public long ProjectId { get; set; }
    public string? OwnerUserId { get; set; }
}
