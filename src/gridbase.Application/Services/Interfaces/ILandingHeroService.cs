using gridbase.DTO.DTOs;

namespace gridbase.Application.Services.Interfaces;


public interface ILandingHeroService
{
    Task<LandingHeroResponseDto?> GetConfigAsync();
    Task UpdateConfigAsync(UpdateLandingHeroDto dto);
}
