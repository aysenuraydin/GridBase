using Microsoft.AspNetCore.Identity;

namespace gridbase.Infrastructure.Identity;

public class ApplicationRole : IdentityRole
{//10
    public ApplicationRole()
    {
    }

    public ApplicationRole(string roleName) : base(roleName)
    {
    }
}