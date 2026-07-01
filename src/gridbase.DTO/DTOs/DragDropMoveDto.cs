
using System.ComponentModel.DataAnnotations;
namespace gridbase.DTO.DTOs;

public class DragDropMoveDto
{
    [Required(ErrorMessage = "Start date is required.")]
    public DateTime Start { get; set; }

    [Required(ErrorMessage = "End date is required.")]
    public DateTime End { get; set; }
}