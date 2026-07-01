
using gridbase.DTO.DTOs;

namespace gridbase.Application.Services.Interfaces;


public interface IServiceSectionService
{
    Task<ServiceSectionDto> GetAsync();
    Task<ServiceSectionDto> UpsertAsync(ServiceSectionDto dto);
}
