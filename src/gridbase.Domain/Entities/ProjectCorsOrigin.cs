using gridbase.Domain.Common;

namespace gridbase.Domain.Entities;

public class ProjectCorsOrigin : BaseAuditableEntity<long>
{
    public long ProjectId { get; private set; }
    public Project ProjectFk { get; private set; } = null!;

    public string Origin { get; private set; } = null!;

    public ProjectCorsOrigin() { }

    public static ProjectCorsOrigin Create(long projectId, string origin)
    {
        if (projectId <= 0)
            throw new DomainException("Gecerli bir proje gerekli.");
        if (string.IsNullOrWhiteSpace(origin))
            throw new DomainException("Origin bos olamaz.");

        return new ProjectCorsOrigin
        {
            ProjectId = projectId,
            Origin = Normalize(origin)
        };
    }

    public static string Normalize(string origin)
    {
        var o = origin.Trim().TrimEnd('/');
        return o == "*" ? "*" : o.ToLowerInvariant();
    }

    public void HardDelete()
    {
        IsHardDelete = true;
    }
}