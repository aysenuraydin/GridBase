using AutoMapper;
using MediatR;
using gridbase.Application.Common.Behaviors;
using gridbase.Application.Common.Interfaces;
using gridbase.Application.Common.Models;
using gridbase.Domain.Common;
using gridbase.DTO.DTOs;

namespace gridbase.Application.Features.TableCells.Queries.GetTableColumnTableById;

public record GetFilteredColumnCellsByTableIdQuery(long TableId) : IRequest<Result<List<TableColumnWithCellsDto>>>, ITableScopedRequest
{
    public TableAccessType AccessType => TableAccessType.Read;
    public long? TableIdHint => TableId;
}
public class GetFilteredColumnCellsByTableIdQueryHandler : IRequestHandler<GetFilteredColumnCellsByTableIdQuery, Result<List<TableColumnWithCellsDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppCache _redisCache;

    public GetFilteredColumnCellsByTableIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppCache redisCache)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _redisCache = redisCache;
    }

    public async Task<Result<List<TableColumnWithCellsDto>>> Handle(GetFilteredColumnCellsByTableIdQuery request, CancellationToken cancellationToken)
    {
        // 🔒 Hidden. Akış: filtre kolon id'lerini bul → bu kolonların dolu/silinmemiş
        //   hücrelerini kolona göre gruplayıp DTO'ya map'le → boşsa Failure → Result.
        throw new NotImplementedException("Source available on request.");
    }
}