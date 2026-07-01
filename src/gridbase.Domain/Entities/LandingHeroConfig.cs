using gridbase.Domain.Common;

namespace gridbase.Domain.Entities;

public class LandingHeroConfig
{
    public int Id { get; set; }

    public string Title { get; set; } = "GridBase";

    public string Description { get; set; } = "Veri odaklı iş süreçlerinizi yönetin, potansiyelinizi açığa çıkarın.";

    public List<HeroSliderImage> SliderImages { get; set; } = new();


}
