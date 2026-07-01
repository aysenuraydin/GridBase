using gridbase.Application.Interfaces;
using gridbase.Application.Services.Interfaces;
using gridbase.Domain.Enums;
using gridbase.DTO.DTOs;

namespace gridbase.Application.Services;

public partial class GridBaseService
{
    private const TableViewType DefaultViewType = TableViewType.List;
    private const ModalSizeType DefaultModalSize = ModalSizeType.Md;
    private const int DefaultPageSize = 10;

    public async Task<IReadOnlyList<TableSummaryResponse>> GetAllTablesAsync(
        IReadOnlyList<FilterDescriptor>? filters = null,
        SortDescriptor? sort = null,
        SelectDescriptor? select = null,
        CancellationToken ct = default)
    {
        // 🔒 Hidden. Akış: tabloları çek → alan-haritası üzerinden filtre →
        //   tip-duyarlı sırala → özet DTO + select.
        throw new NotImplementedException("Source available on request.");
    }

    public async Task<TableSummaryResponse?> GetOneTableAsync(
        IReadOnlyList<FilterDescriptor>? filters = null,
        SortDescriptor? sort = null,
        SelectDescriptor? select = null,
        CancellationToken ct = default)
    {
        // 🔒 Hidden. GetAllTables → ilk sonuç.
        throw new NotImplementedException("Source available on request.");
    }

    public async Task<TableSummaryResponse?> GetTableByIdAsync(long tableId, CancellationToken ct = default)
    {
        // 🔒 Hidden. Id ile tabloyu çek → özet DTO.
        throw new NotImplementedException("Source available on request.");
    }

    public async Task<TableSummaryResponse> CreateTableAsync(CreateTableRequest request, CancellationToken ct = default)
    {
        // 🔒 Hidden. Akış: yönetim yetkisi → ad doğrula/benzersizlik →
        //   Datatable.Create → ekle/kaydet → özet DTO.
        throw new NotImplementedException("Source available on request.");
    }

    public async Task<TableSummaryResponse?> UpdateTableAsync(long tableId, UpdateTableRequest request, CancellationToken ct = default)
    {
        // 🔒 Hidden. Akış: yetki → tabloyu bul → ad değişiyorsa benzersizlik →
        //   alanları güncelle → kaydet → özet DTO.
        throw new NotImplementedException("Source available on request.");
    }

    public async Task<bool> DeleteTableAsync(long tableId, bool hard = false, CancellationToken ct = default)
    {
        // 🔒 Hidden. Akış: yetki → tabloyu bul → LockedTables ise reddet →
        //   hard ise kalıcı sil, değilse soft-delete → kaydet.
        throw new NotImplementedException("Source available on request.");
    }
}
