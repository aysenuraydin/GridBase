using AutoMapper;
using MediatR;
using gridbase.Application.Common.Interfaces;
using gridbase.Application.Common.Models;
using gridbase.DTO.DTOs;

namespace gridbase.Application.Features.Datatables.Queries.GetTables;

public class GetPaginatedDatatableQuery : IRequest<PaginatedResult<DatatableDto>>
{
    public string? Search { get; set; } = string.Empty;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
public class GetPaginatedDatatableQueryHandler : IRequestHandler<GetPaginatedDatatableQuery, PaginatedResult<DatatableDto>>
{
    private readonly IGridBaseDbContext _db;
    private readonly IMapper _mapper;
    private readonly IAppCache _redisCache;

    public GetPaginatedDatatableQueryHandler(IGridBaseDbContext db, IMapper mapper, IAppCache redisCache)
    {
        _db = db;
        _mapper = mapper;
        _redisCache = redisCache;
    }

    public async Task<PaginatedResult<DatatableDto>> Handle(GetPaginatedDatatableQuery request, CancellationToken cancellationToken)
    {
        // 🔒 Hidden. Akış: arama+sayfa bazlı cache key → cache-aside ile aktif
        //   tabloları projeksiyonla sorgula → arama filtresi uygula →
        //   PaginatedResult.Create → döndür.
        throw new NotImplementedException("Source available on request.");
    }
}