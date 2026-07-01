using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using gridbase.Application.Common.Interfaces;
using gridbase.Domain.Constants;
using gridbase.Infrastructure.Persistence.Common;

namespace gridbase.Infrastructure.Identity;

public class IdentitySeeder : ISeeder
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public IdentitySeeder(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    // NOT: Bunlar yalnızca yerel demo/başlangıç hesaplarıdır.
    // Gerçek bir ortamda güçlü ve gizli değerlerle değiştirilmelidir.
    private const string UserUserName = "demo.user";
    private const string UserEmail = "user@demo.local";
    private const string UserPassword = "Demo!User123";

    private const string GBUserName = "demo.gb";
    private const string GBEmail = "gb@demo.local";
    private const string GBPassword = "Demo!Gb123456";

    public async Task Seed(IGridBaseDbContext context)
    {
        if (await _userManager.Users.AnyAsync()) return;

        foreach (var roleName in new[] { Roles.GB, Roles.User })
        {
            if (_roleManager.Roles.All(r => r.Name != roleName))
                await _roleManager.CreateAsync(new ApplicationRole(roleName));
        }

        await CreateUserAsync(
            userName: UserUserName,
            email: UserEmail,
            password: UserPassword,
            firstName: "GridBase",
            lastName: "User",
            role: Roles.User);

        await CreateUserAsync(
            userName: GBUserName,
            email: GBEmail,
            password: GBPassword,
            firstName: "GridBase",
            lastName: "GB",
            role: Roles.GB);
    }

    private async Task CreateUserAsync(
        string userName, string email, string password,
        string firstName, string lastName, string role)
    {
        if (_userManager.Users.Any(u => u.UserName == userName)) return;

        var user = new ApplicationUser
        {
            UserName = userName,
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            JoiningDate = DateTime.Now.ToString("dd.MM.yyyy"),
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            var errors = string.Join(" | ", result.Errors.Select(e => e.Description));
            throw new Exception($"IdentitySeeder - '{userName}' oluşturulamadı: {errors}");
        }

        await _userManager.AddToRolesAsync(user, new[] { role });
    }
}