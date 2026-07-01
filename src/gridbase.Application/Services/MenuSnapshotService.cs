
using gridbase.Application.Common.Interfaces;
using gridbase.Domain.Common;
using gridbase.DTO.DTOs;

namespace gridbase.Application.Services;

public class MenuSnapshotService
{
    private readonly IGridBaseDbContext _db;
    private readonly IUnitOfWork _unitOfWork;
    public MenuSnapshotService(IGridBaseDbContext db, IUnitOfWork unitOfWork)
    {
        _db = db;
        _unitOfWork = unitOfWork;
    }

    public async Task<SaveMenuSnapshotResult> SaveAsync(string? savedBy = null)
    {
        // 🔒 Hidden. Akış: menü öğelerini (badge dahil) DTO'ya projekte et →
        //   JSON serialize → tek snapshot kaydını oluştur/güncelle.
        throw new NotImplementedException("Source available on request.");
    }

    public async Task<GetMenuSnapshotResult> GetAsync()
    {
        // 🔒 Hidden. Akış: snapshot yoksa Exists=false; varsa JSON'u deserialize et.
        throw new NotImplementedException("Source available on request.");
    }

    public async Task<RestoreMenuSnapshotResult> RestoreAsync()
    {
        // 🔒 Hidden. Akış: snapshot'ı çöz → mevcut menüyü temizle →
        //   hiyerarşik sırada yeniden oluştur → eski→yeni id eşlemesiyle parent bağlarını kur.
        throw new NotImplementedException("Source available on request.");
    }
}