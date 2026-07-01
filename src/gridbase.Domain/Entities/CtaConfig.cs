using gridbase.Domain.Common;

namespace gridbase.Domain.Entities;

public class CtaConfig
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public string ButtonText { get; set; } = string.Empty;
    public string ButtonUrl { get; set; } = string.Empty;
}