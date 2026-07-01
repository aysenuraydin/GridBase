
using gridbase.DTO.DTOs;

namespace gridbase.Application.Services.Interfaces;

public interface IBrandService
{
    Task<BrandConfigDto> GetAsync();
    Task<BrandConfigDto> UpsertAsync(BrandConfigDto dto);
}