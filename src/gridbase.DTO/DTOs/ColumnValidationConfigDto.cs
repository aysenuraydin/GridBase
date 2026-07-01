using gridbase.Domain.Enums;
namespace gridbase.DTO.DTOs;

public class ColumnValidationConfigDto
{
    public FieldTypeEnum Type { get; set; }
    public List<RulesValidationConfigDto> Rules { get; set; } = new();
}


