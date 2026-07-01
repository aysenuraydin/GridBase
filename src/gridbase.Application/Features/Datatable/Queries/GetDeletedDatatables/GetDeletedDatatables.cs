using AutoMapper;
using MediatR;
using gridbase.Application.Common.Interfaces;
using gridbase.Application.Common.Models;
using gridbase.Domain.Common;
using gridbase.DTO.DTOs;

namespace gridbase.Application.Features.Datatables.Queries.GetTables;

public record GetDeletedDatatablesQuery : IRequest<Result<List<DatatableDto>>>;
public class GetDeletedDatatablesQueryHandler : IRequestHandler<GetDeletedDatatablesQuery, Result<List<DatatableDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppCache _redisCache;

    public GetDeletedDatatablesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppCache redisCache)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _redisCache = redisCache;
    }

    public async Task<Result<List<DatatableDto>>> Handle(GetDeletedDatatablesQuery request, CancellationToken cancellationToken)
    {
        // 🔒 Hidden. Akış: cache key → cache-aside ile silinmiş tabloları ProjectTo
        //   ile çek → boşsa Failure → Result.
        throw new NotImplementedException("Source available on request.");
    }
}