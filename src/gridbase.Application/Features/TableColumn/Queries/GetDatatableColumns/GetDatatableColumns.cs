using AutoMapper;
using MediatR;
using gridbase.Application.Common.Interfaces;
using gridbase.Application.Common.Models;
using gridbase.Domain.Common;
using gridbase.DTO.DTOs;

namespace gridbase.Application.Features.TableColumns.Queries.GetTableColumnTableById;

public class GetDatatableColumnsQuery : IRequest<Result<List<DatatablesWithColumnsDto>>> { }
public class GetDatatableColumnsQueryHandler : IRequestHandler<GetDatatableColumnsQuery, Result<List<DatatablesWithColumnsDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppCache _redisCache;

    public GetDatatableColumnsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppCache redisCache)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _redisCache = redisCache;
    }

    public async Task<Result<List<DatatablesWithColumnsDto>>> Handle(GetDatatableColumnsQuery request, CancellationToken cancellationToken)
    {
        // 🔒 Hidden. Akış: cache key → cache-aside ile gerçek kolonları (silinmemiş,
        //   RealTableId null) tabloya göre grupla → DTO'ya map'le → boşsa Failure → Result.
        throw new NotImplementedException("Source available on request.");
    }
}