using gridbase.Domain.Enums;

namespace gridbase.Domain.Common;

public static class PlanLimits
{
    public record Limits(
        int MaxProjects,
        int MaxTablesPerProject,
        int MaxStorageMb);

    private static readonly Limits Free = new(
        MaxProjects: 2,
        MaxTablesPerProject: 50,
        MaxStorageMb: 100);

    private static readonly Limits Pro = new(
        MaxProjects: 999,
        MaxTablesPerProject: 9999,
        MaxStorageMb: 10240);

    public static Limits For(PlanType plan) => plan switch
    {
        PlanType.Pro => Pro,
        _ => Free
    };
}