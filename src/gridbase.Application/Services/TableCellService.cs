using AutoMapper;
using Microsoft.EntityFrameworkCore;
using gridbase.Application.Common.Models;
using gridbase.Application.Common.Services;
using gridbase.Application.Services.Interfaces;
using gridbase.Domain.Common;
using gridbase.Domain.Entities;
using AutoMapper.QueryableExtensions;

namespace gridbase.Application.Services;

public class TableCellService : BaseService<TableCell>, ITableCellService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public TableCellService(IRepository<TableCell, long> repository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<List<TableCell>>> GetAll()
    {
        var entities = await _unitOfWork.TableCellRepository.GetAll().ToListAsync();
        return Result<List<TableCell>>.Success(_mapper.Map<List<TableCell>>(entities));
    }
    public async Task<PaginatedResult<TableCell>> GetPaginatedNormal(PaginationRequest req)
    {
        var entityQuery = _unitOfWork.TableCellRepository.GetAll()
            .Include(e => e.ColumnId)
            .OrderByDescending(c => c.Id);

        var totalEntity = await entityQuery.CountAsync();
        var pagedEntities = await entityQuery.Skip((req.PageNumber - 1) * req.PageSize).AsNoTracking().ToListAsync();
        var pagedDtos = _mapper.Map<List<TableCell>>(pagedEntities);

        return new PaginatedResult<TableCell>(pagedDtos, totalEntity, req.PageNumber, req.PageSize);
    }

    public async Task<PaginatedResult<TableCell>> GetPaginated(PaginationRequest req)
    {
        var entities = _unitOfWork.TableCellRepository.GetAll()
                .Include(e => e.ColumnId)
                .OrderByDescending(e => e.Id)
                .ProjectTo<TableCell>(_mapper.ConfigurationProvider)
                .AsNoTracking();

        return await PaginatedResult<TableCell>.Create(entities, req.PageNumber, req.PageSize);
    }
    public async Task<Result<TableCell?>> GetById(long id)
    {
        var entity = await _unitOfWork.TableCellRepository
            .GetAll(
                e => e.ColumnId
                ).FirstOrDefaultAsync(e => e.Id == id);

        if (entity == null)
            return Result<TableCell?>.Failure("Record not found");

        return Result<TableCell?>.Success(_mapper.Map<TableCell>(entity));
    }
    public async Task<Result<TableCell?>> GetFormById(long id)
    {
        var entity = await _unitOfWork.TableCellRepository.GetById(id);
        if (entity == null)
            return Result<TableCell?>.Failure("Record not found");
        return Result<TableCell?>.Success(_mapper.Map<TableCell>(entity));
    }
    public async Task<Result<long>> Create(TableCell dto)
    {
        var entity = _mapper.Map<TableCell>(dto);
        await _unitOfWork.TableCellRepository.Create(entity);
        return Result<long>.Success(entity.Id, "Created!");
    }

    public async Task<Result<bool>> Update(TableCell dto)
    {
        var entity = _mapper.Map<TableCell>(dto);
        await _unitOfWork.TableCellRepository.Update(entity);
        await _unitOfWork.CommitAsync();
        return Result<bool>.Success(true, "Updated!");
    }
    public async Task<Result<bool>> Delete(long id)
    {
        await _unitOfWork.TableCellRepository.DeleteById(id);
        await _unitOfWork.CommitAsync();
        return Result<bool>.Success(true, "Deleted!");
    }

}