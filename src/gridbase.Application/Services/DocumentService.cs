using gridbase.Application.Services.Interfaces;
using gridbase.Domain.Repositories;
using gridbase.DTO.DTOs;

namespace gridbase.Application.Services;

public class DocumentService : IDocumentService
{
    private readonly IDocumentRepository _repo;
    public DocumentService(IDocumentRepository repo) => _repo = repo;

    public async Task<DocumentDto> GetAsync()
    {
        // 🔒 Hidden. Kaydı çek → yoksa boş DTO → map.
        throw new NotImplementedException("Source available on request.");
    }

    public async Task<DocumentDto> UpsertAsync(DocumentDto dto)
    {
        // 🔒 Hidden. Kaydı çek/oluştur → açıklamayı güncelle → kaydet → map.
        throw new NotImplementedException("Source available on request.");
    }
}