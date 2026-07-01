using gridbase.Domain.Common;

namespace gridbase.Domain.Entities;

public class BrandConfig
{
    public int Id { get; set; }
    public string CompanyName { get; set; } = "GridBase";
    public string Description { get; set; } = "Taking inspiration from world-class innovators, we cooperate with leading enterprises to streamline business operations and build seamless digital platforms. ";

    public string Website { get; set; } = "wwww.GridBase.com";
}
