using gridbase.Application.Services.Interfaces;
using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;
using gridbase.DTO.DTOs;

namespace gridbase.Application.Services;

public class LandingHeroService : ILandingHeroService
{
    private readonly ILandingHeroRepository _repo;

    public LandingHeroService(ILandingHeroRepository repo)
    {
        _repo = repo;
    }
    public async Task<LandingHeroResponseDto?> GetConfigAsync()
    {
        var config = await _repo.GetWithImagesAsync();
        if (config is null)
        {
            config = new LandingHeroConfig();
            await _repo.AddAsync(config);
        }

        return new LandingHeroResponseDto
        {
            Title = config.Title,
            Description = config.Description,
            SliderImages = config.SliderImages
                .OrderBy(i => i.OrderNumber)
                .Select(i => i.ImageUrl)
                .ToList(),
        };
    }

    public async Task UpdateConfigAsync(UpdateLandingHeroDto dto)
    {
        var config = await _repo.GetWithImagesAsync();

        if (config is null)
        {
            config = new LandingHeroConfig();
            await _repo.AddAsync(config);
        }

        config.Title = dto.Title;
        config.Description = dto.Description;

        _repo.RemoveImages(config.SliderImages);

        int order = 1;
        foreach (var url in dto.SliderImages)
        {
            config.SliderImages.Add(new HeroSliderImage
            {
                ImageUrl = url,
                OrderNumber = order++,
            });
        }

        await _repo.SaveChangesAsync();
    }
}

