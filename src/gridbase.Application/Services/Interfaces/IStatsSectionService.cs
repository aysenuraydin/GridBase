using gridbase.DTO.DTOs;

namespace gridbase.Application.Services.Interfaces;

public interface IStatsSectionService
{
    Task<StatsSectionDto> GetAsync();
    Task<StatsSectionDto> UpsertAsync(StatsSectionDto dto);
}