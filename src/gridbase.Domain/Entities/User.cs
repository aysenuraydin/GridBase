using gridbase.Domain.Enums;
namespace gridbase.Domain.Entities;

public class User : BaseEntity<Guid>
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Roles { get; set; }
    public GenderEnum? Gender { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? ActivationKey { get; set; }
    public bool IsActive { get; set; }
    public string? ProfilePictureUrl { get; set; }

}