using gridbase.Domain.Enums;

namespace gridbase.DTO.DTOs;

public record SetTableAccessRequest(
    AccessLevel ReadAccess,
    AccessLevel WriteAccess,
    string? ReadRequiredRole = null,
    string? WriteRequiredRole = null,
    bool IsOwnerScoped = false,
    string? OwnerColumn = null);


