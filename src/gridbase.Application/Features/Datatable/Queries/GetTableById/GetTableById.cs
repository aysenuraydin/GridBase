using AutoMapper;
using MediatR;
using gridbase.Application.Common.Interfaces;
using gridbase.Application.Common.Models;
using gridbase.Domain.Common;
using gridbase.DTO.DTOs;

namespace gridbase.Application.Features.Tables.Queries.GetTableById;

public class GetTableByIdQuery : IRequest<Result<TableDto>>
{
    public long TableId { get; set; }
    public GetTableByIdQuery(long id) => TableId = id;
}
public class GetTableByIdQueryHandler : IRequestHandler<GetTableByIdQuery, Result<TableDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppCache _redisCache;

    public GetTableByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppCache redisCache)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _redisCache = redisCache;
    }

    public async Task<Result<TableDto>> Handle(GetTableByIdQuery request, CancellationToken cancellationToken)
    {
        // 🔒 Hidden. Akış: id'li cache key → cache-aside ile tabloyu ProjectTo ile
        //   çek → yoksa Failure → Result.
        throw new NotImplementedException("Source available on request.");
    }
}