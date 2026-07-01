using gridbase.Domain.Enums;

namespace gridbase.DTO.DTOs;

public sealed class CreateProjectRequest
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}

public sealed class UpdateProjectRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}

public sealed class ProjectResponse
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public PlanType Plan { get; set; }
    public int TableCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public sealed class ProjectListItem
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public PlanType Plan { get; set; }
    public int TableCount { get; set; }
    public DateTime CreatedAt { get; set; }
}

public sealed class ProjectQuotaResponse
{
    public int Used { get; set; }
    public int Max { get; set; }
    public bool CanCreate { get; set; }
    public PlanType Plan { get; set; }
}