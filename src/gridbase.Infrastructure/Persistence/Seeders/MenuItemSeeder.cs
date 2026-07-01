using gridbase.Application.Common.Interfaces;
using gridbase.Domain.Entities;
using gridbase.Infrastructure.Persistence.Common;

namespace gridbase.Infrastructure.Persistence.Seeders;

public class MenuItemSeeder : ISeeder
{
    private IGridBaseDbContext _ctx = null!;
    private int _order = 1;

    public async Task Seed(IGridBaseDbContext context)
    {
        if (context.MenuItems.Any()) return;

        _ctx = context;

        await AddLink("Genel Bakış", "/overview", "bx bx-grid-alt", null);
        await AddLink("Projelerim", "/projects", "bx bx-folder", null);

        await AddDivider("Proje");
        await AddLink("Tablolar", "/datatables", "bx bx-table", null);
        await AddLink("API Console", "/console", "ri-flashlight-line", null);
        await AddLink("Storage", "/storage", "bx bx-folder-open", null);
        await AddLink("API Keys", "/keys", "bx bx-key", null);
        await AddLink("Ayarlar", "/project-settings", "bx bx-cog", null);
    }

    private Task<MenuItem> AddDivider(string label, bool isAdmin = false) =>
        Save(MenuItem.Create(
            label: label, order: _order++, link: null, icon: null,
            visible: true, isHeader: true, parentId: null,
            locked: false, isAdmin: isAdmin, badgeName: null, badgeColor: null));

    private Task<MenuItem> AddParent(string label, string? icon, long? parentId = null, bool isAdmin = false) =>
        Save(MenuItem.Create(
            label: label, order: _order++, link: null, icon: icon,
            visible: true, isHeader: false, parentId: parentId,
            locked: false, isAdmin: isAdmin, badgeName: null, badgeColor: null));

    private Task<MenuItem> AddLink(
        string label, string link, string? icon, long? parentId,
        string? badgeName = null, string? badgeColor = null, bool isAdmin = false) =>
        Save(MenuItem.Create(
            label: label, order: _order++, link: link, icon: icon,
            visible: true, isHeader: false, parentId: parentId,
            locked: false, isAdmin: isAdmin, badgeName: badgeName, badgeColor: badgeColor));

    private async Task<MenuItem> Save(MenuItem item)
    {
        await _ctx.MenuItems.AddAsync(item);
        await _ctx.SaveChangesAsync();
        return item;
    }
}