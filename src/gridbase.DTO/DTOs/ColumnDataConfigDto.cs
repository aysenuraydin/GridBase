using gridbase.Domain.Enums;
namespace gridbase.DTO.DTOs;

public class ColumnDataConfigDto
{
    public long ColumnId { get; set; }
    public string? Value { get; set; } = null!;
    public PropertyEnum Type { get; set; }
};