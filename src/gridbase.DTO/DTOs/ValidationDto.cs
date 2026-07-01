using gridbase.Domain.Enums;

public class ValidationRuleDto
{
    public ValidationRuleEnum Rule { get; set; }
    public bool? IsActive { get; set; } = true;
    public string? Value { get; set; }
    public string? Message { get; set; }
}

public class SetColumnValidationRequest
{
    public FieldTypeEnum Type { get; set; }
    public List<ValidationRuleDto> Rules { get; set; } = new();
}

public class ColumnValidationResponse
{
    public string Column { get; set; } = "";
    public FieldTypeEnum Type { get; set; }
    public List<ValidationRuleDto> Rules { get; set; } = new();
}