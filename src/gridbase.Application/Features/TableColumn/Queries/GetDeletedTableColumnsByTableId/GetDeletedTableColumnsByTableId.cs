using AutoMapper;
using MediatR;
using gridbase.Application.Common.Interfaces;
using gridbase.Application.Common.Models;
using gridbase.Domain.Common;
using gridbase.DTO.DTOs;

namespace gridbase.Application.Features.TableColumns.Queries.GetDeletedTableColumnTableById;

public class GetDeletedTableColumnsByTableIdQuery : IRequest<Result<List<DeletedTableColumnsDto>>>
{
    public long TableId { get; set; }
    public GetDeletedTableColumnsByTableIdQuery(long id) => TableId = id;
}
public class GetDeletedTableColumnsByTableIdQueryHandler : IRequestHandler<GetDeletedTableColumnsByTableIdQuery, Result<List<DeletedTableColumnsDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppCache _redisCache;

    public GetDeletedTableColumnsByTableIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppCache redisCache)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _redisCache = redisCache;
    }

    public async Task<Result<List<DeletedTableColumnsDto>>> Handle(GetDeletedTableColumnsByTableIdQuery request, CancellationToken cancellationToken)
    {
        // 🔒 Hidden. Akış: tablo bazlı cache key → cache-aside ile silinmiş kolonları
        //   ProjectTo ile çek → boşsa Failure → Result.
        throw new NotImplementedException("Source available on request.");
    }
}