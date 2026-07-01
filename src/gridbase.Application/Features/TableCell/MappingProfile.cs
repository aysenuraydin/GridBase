using AutoMapper;
using gridbase.Application.Features.Datatables.Commands.CreateDatatable;
using gridbase.Application.Features.Datatables.Commands.UpdateDatatable;
using gridbase.Application.Mapping;
using gridbase.Domain.Entities;
using gridbase.DTO.DTOs;

namespace gridbase.Application.Features.TableCells;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TableCellDto, TableCell>().ReverseMap();

        CreateMap<string, DateOnly>().ConvertUsing(new DateTimeTypeConverter());
    }
}
