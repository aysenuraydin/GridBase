namespace gridbase.DTO.DTOs;

public sealed class ProjectOverviewResponse
{
    public long ProjectId { get; set; }
    public string ProjectName { get; set; } = null!;
    public string Plan { get; set; } = null!;

    public int TableCount { get; set; }
    public int TotalRows { get; set; }
    public int FileCount { get; set; }
    public long StorageBytes { get; set; }
    public int ActiveKeyCount { get; set; }

    public int MaxTables { get; set; }
    public int MaxStorageMb { get; set; }

    public List<OverviewTableItem> RecentTables { get; set; } = new();

    public DateTime CreatedAt { get; set; }
}

public sealed class OverviewTableItem
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public int RowCount { get; set; }
    public int ColumnCount { get; set; }
}