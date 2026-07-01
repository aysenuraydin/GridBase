using gridbase.Domain.Enums;
namespace gridbase.DTO.DTOs;

public class TableColumnBulkCreateDto
{
    public InputTypeEnum Type { get; set; }
    public string Name { get; set; } = null!;
    public int TableOrder { get; set; } = 0;
    public bool IsVisible { get; set; } = false;
    public bool IsFilter { get; set; } = false;
}
