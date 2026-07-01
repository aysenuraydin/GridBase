using gridbase.Domain.Entities;

namespace gridbase.Domain.Repositories;

public interface ILandingHeroRepository
{
    Task<LandingHeroConfig?> GetWithImagesAsync();
    Task<LandingHeroConfig> AddAsync(LandingHeroConfig config);
    Task SaveChangesAsync();
    void RemoveImages(IEnumerable<HeroSliderImage> images);
}
