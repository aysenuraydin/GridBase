using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using gridbase.Application.Services.Interfaces;
using gridbase.Infrastructure.Identity;

namespace gridbase.Infrastructure.Services;

public class RoleService : IRoleService
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    public RoleService(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<List<RoleResponse>> GetRolesAsync()
    {
        return await _roleManager.Roles
            .Select(r => new RoleResponse
            {
                Id = r.Id,
                Name = r.Name ?? ""
            })
            .ToListAsync();
    }

    public async Task CreateRoleAsync(CreateRoleRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new Exception("Role name cannot be empty.");

        var roleExist = await _roleManager.RoleExistsAsync(request.Name);
        if (roleExist) throw new Exception($"Role '{request.Name}' already exists.");

        var newRole = new ApplicationRole(request.Name)
        {
            NormalizedName = request.Name.ToUpper()
        };

        var result = await _roleManager.CreateAsync(newRole);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"Failed to create role: {errors}");
        }
    }
    public async Task UpdateRoleAsync(UpdateRoleRequest request)
    {
        var role = await _roleManager.FindByIdAsync(request.Id);
        if (role == null) throw new Exception("Role not found.");

        var roleNameLower = role.Name?.ToLower();
        if (roleNameLower == "admin" || roleNameLower == "user" || roleNameLower == "gb")
            throw new Exception($"System core roles (Admin, User, GB) cannot be modified.");

        role.Name = request.Name;
        role.NormalizedName = request.Name.ToUpper();

        var result = await _roleManager.UpdateAsync(role);
        if (!result.Succeeded) throw new Exception("Failed to update role.");
    }
    public async Task DeleteRoleAsync(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null) throw new Exception("Role not found.");

        var roleNameLower = role.Name?.ToLower();
        if (roleNameLower == "admin" || roleNameLower == "user" || roleNameLower == "gb")
            throw new Exception($"System core roles (Admin, User, GB) are protected and cannot be deleted!");

        var result = await _roleManager.DeleteAsync(role);
        if (!result.Succeeded) throw new Exception("Failed to delete role.");
    }
}