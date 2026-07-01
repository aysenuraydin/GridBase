using gridbase.Domain.Enums;
namespace gridbase.DTO.DTOs;

public record CreateBulkTableRowDto(List<TableCellDto> CellsFk);