using gridbase.DTO.DTOs;

namespace gridbase.Application.Services.Interfaces;

public interface IAboutConfigService
{
    Task<AboutConfigDto> GetAsync();
    Task<AboutConfigDto> UpsertAsync(AboutConfigDto dto);
}
