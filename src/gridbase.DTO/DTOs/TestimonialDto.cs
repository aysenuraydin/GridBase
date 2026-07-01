namespace gridbase.DTO.DTOs;

public record TestimonialDto(
    string Id,
    string Name,
    string Role,
    string Comment,
    string AvatarUrl,
    int Rating
);
