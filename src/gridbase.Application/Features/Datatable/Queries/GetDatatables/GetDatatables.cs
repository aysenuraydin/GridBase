using AutoMapper;
using MediatR;
using gridbase.Application.Common.Interfaces;
using gridbase.Application.Common.Models;
using gridbase.Domain.Common;
using gridbase.DTO.DTOs;

namespace gridbase.Application.Features.Datatables.Queries.GetTables;

public record GetDatatablesQuery : IRequest<Result<List<DatatableDto>>>;
public class GetDatatableQueryHandler : IRequestHandler<GetDatatablesQuery, Result<List<DatatableDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppCache _redisCache;

    public GetDatatableQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppCache redisCache)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _redisCache = redisCache;
    }

    public async Task<Result<List<DatatableDto>>> Handle(GetDatatablesQuery request, CancellationToken cancellationToken)
    {
        // 🔒 Hidden. Akış: cache key → cache-aside ile aktif tabloları (silinmemiş,
        //   tarihe göre) ProjectTo ile çek → boşsa Failure → Result.
        throw new NotImplementedException("Source available on request.");
    }
}