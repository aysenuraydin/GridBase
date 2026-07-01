using Microsoft.EntityFrameworkCore;
using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;

namespace gridbase.Infrastructure.Persistence.Repositories;

public class LandingHeroRepository : ILandingHeroRepository
{
    private readonly GridBaseDbContext _context;

    public LandingHeroRepository(GridBaseDbContext context)
    {
        _context = context;
    }

    public async Task<LandingHeroConfig?> GetWithImagesAsync()
        => await _context.LandingHeroConfigs
            .Include(h => h.SliderImages)
            .FirstOrDefaultAsync();

    public async Task<LandingHeroConfig> AddAsync(LandingHeroConfig config)
    {
        await _context.LandingHeroConfigs.AddAsync(config);
        return config;
    }

    public void RemoveImages(IEnumerable<HeroSliderImage> images)
        => _context.HeroSliderImages.RemoveRange(images);

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}