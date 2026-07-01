
using gridbase.Domain.Common;
using gridbase.Domain.Enums;

namespace gridbase.Domain.Entities;

public class Project : BaseAuditableEntity<long>
{
    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }
    public string OwnerUserId { get; private set; } = null!;
    public PlanType Plan { get; private set; }

    public Project() { }

    public static Project Create(
        string name,
        string ownerUserId,
        PlanType plan = PlanType.Free,
        string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Proje adı boş olamaz!");

        if (string.IsNullOrWhiteSpace(ownerUserId))
            throw new DomainException("Proje sahibi zorunludur!");

        return new Project
        {
            Name = name.Trim(),
            OwnerUserId = ownerUserId,
            Plan = plan,
            Description = description?.Trim()
        };
    }

    public void Update(string name, string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Proje adı boş olamaz!");

        Name = name.Trim();
        Description = description?.Trim();
    }

    public void ChangePlan(PlanType plan)
    {
        Plan = plan;
    }

    public void Delete()
    {
    }

    public void HardDelete()
    {
        IsHardDelete = true;
    }
}