
using gridbase.DTO.DTOs;

namespace gridbase.Application.Services.Interfaces;

public interface IFaqService
{
    Task<List<FaqCategoryDto>> GetAllAsync();
    Task<List<FaqCategoryDto>> UpsertAsync(List<FaqCategoryDto> dto);
}
