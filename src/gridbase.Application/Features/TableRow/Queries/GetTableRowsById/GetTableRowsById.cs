using AutoMapper;
using MediatR;
using gridbase.Application.Common.Behaviors;
using gridbase.Application.Common.Interfaces;
using gridbase.Application.Common.Models;
using gridbase.Domain.Common;
using gridbase.DTO.DTOs;

namespace gridbase.Application.Features.TableRows.Queries.GetTableColumnTableById;

public class GetTableRowsByIdQuery : IRequest<Result<TableRowsDto>>, ITableScopedRequest
{
    public TableAccessType AccessType => TableAccessType.Read;
    public long? RowIdHint => Id;
    public long Id { get; set; }
    public GetTableRowsByIdQuery(long id) => Id = id;
}
public class GetTableRowsByIdQueryHandler : IRequestHandler<GetTableRowsByIdQuery, Result<TableRowsDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppCache _redisCache;

    public GetTableRowsByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppCache redisCache)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _redisCache = redisCache;
    }

    public async Task<Result<TableRowsDto>> Handle(GetTableRowsByIdQuery request, CancellationToken cancellationToken)
    {
        // 🔒 Hidden. Akış: id'li cache key → cache-aside ile satırı ProjectTo ile çek →
        //   yoksa Failure → Result.
        throw new NotImplementedException("Source available on request.");
    }
}