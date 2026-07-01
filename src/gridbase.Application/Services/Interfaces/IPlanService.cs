
using gridbase.DTO.DTOs;

namespace gridbase.Application.Services.Interfaces;

public interface IPlanService
{
    Task<PlanSectionDto> GetAsync();
    Task<PlanSectionDto> UpsertAsync(PlanSectionDto dto);
}