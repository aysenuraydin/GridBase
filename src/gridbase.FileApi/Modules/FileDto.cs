namespace gridbase.FileApi.Modules;

public class FileDto
{
    public string Name { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public byte[] Data { get; set; } = null!;
}

public class FileListItemDto
{
    public long Id { get; set; }
    public string OriginalName { get; set; } = "";
    public string LocalName { get; set; } = "";
    public string? ContentType { get; set; }
    public string? Extension { get; set; }
    public long Size { get; set; }
    public System.DateTime CreatedAt { get; set; }
}

public class FileListResult
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
    public int TotalPages { get; set; }
    public System.Collections.Generic.List<FileListItemDto> Items { get; set; } = new();
}
