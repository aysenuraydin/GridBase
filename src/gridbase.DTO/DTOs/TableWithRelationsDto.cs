using gridbase.Domain.Enums;

namespace gridbase.DTO.DTOs;

public class TableWithRelationsDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<TableWithRelationsColumnDto> ColumnsFk { get; set; } = new List<TableWithRelationsColumnDto>();
    public List<ForeignTableDto>? ForeignTablesFk { get; set; }
}


