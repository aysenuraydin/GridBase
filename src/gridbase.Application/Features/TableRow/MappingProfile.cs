using AutoMapper;
using gridbase.Application.Features.TableRows.Commands.CreateTableRow;
using gridbase.Application.Mapping;
using gridbase.Domain.Entities;
using gridbase.DTO.DTOs;

namespace gridbase.Application.Features.TableRows;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TableRow, CreateTableRowCommand>().ReverseMap();

        CreateMap<DatatableRowsDto, TableRow>().ReverseMap();
        CreateMap<TableRowsDto, TableRow>().ReverseMap();

        CreateMap<string, DateOnly>().ConvertUsing(new DateTimeTypeConverter());
    }
}
