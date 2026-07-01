namespace gridbase.DTO.DTOs;

public class AddRelationRequest

{
    public string ToTable { get; set; } = null!;
    public bool IsMultiSelect { get; set; }
}
