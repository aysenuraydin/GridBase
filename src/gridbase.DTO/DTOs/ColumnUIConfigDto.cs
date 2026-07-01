using gridbase.Domain.Enums;
namespace gridbase.DTO.DTOs;

public class ColumnUIConfigDto
{
    public long ColumnId { get; set; }
    public string? Value { get; set; } = null!;
    public AttributeEnum Type { get; set; }
};
