using gridbase.DTO.DTOs;

namespace gridbase.Application.Services.Interfaces;

public interface IDocumentService
{
    Task<DocumentDto> GetAsync();
    Task<DocumentDto> UpsertAsync(DocumentDto dto);
}
