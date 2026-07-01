namespace gridbase.Application.Common.Interfaces;

public enum ProjectSource
{
    None = 0,
    Jwt = 1,
    AnonKey = 2,
    SecretKey = 3
}

public interface IProjectContext
{
    long? ProjectId { get; }

    ProjectSource Source { get; }

    bool IsServiceLevel { get; }

    string? OwnerUserId { get; }

    bool HasProject { get; }

    void Set(long projectId, ProjectSource source, bool isServiceLevel, string? ownerUserId);
    void Clear();
}