using gridbase.Domain.Enums;
namespace gridbase.DTO.DTOs;

public class DatatablesWithColumnsDto
{
    public long TableId { get; set; }
    public List<DatatableColumnsNamesDto> Columns { get; set; } = null!;
}
