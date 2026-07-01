using gridbase.Application.Common.Interfaces;

namespace gridbase.Infrastructure.Context;

public class ProjectContext : IProjectContext
{
    public long? ProjectId { get; private set; }
    public ProjectSource Source { get; private set; } = ProjectSource.None;
    public bool IsServiceLevel { get; private set; }
    public string? OwnerUserId { get; private set; }

    public bool HasProject => ProjectId.HasValue;

    public void Set(long projectId, ProjectSource source, bool isServiceLevel, string? ownerUserId)
    {
        ProjectId = projectId;
        Source = source;
        IsServiceLevel = isServiceLevel;
        OwnerUserId = ownerUserId;
    }

    public void Clear()
    {
        ProjectId = null;
        Source = ProjectSource.None;
        IsServiceLevel = false;
        OwnerUserId = null;
    }
}