namespace gridbase.DTO.DTOs;


public record ServiceSectionDto(
    string MainTitle,
    string MainDescription,
    List<ServiceItemDto> Items);


