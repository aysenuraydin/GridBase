using gridbase.Application.Services.Interfaces;
using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;
using gridbase.DTO.DTOs;

namespace gridbase.Application.Services;

public class ServiceSectionService(IServiceRepository repo) : IServiceSectionService
{
    public async Task<ServiceSectionDto> GetAsync()
    {
        var section = await repo.GetWithItemsAsync();
        return section is null
            ? new ServiceSectionDto("Hizmetlerimiz", "Henüz açıklama eklenmedi", new())
            : MapSection(section);
    }

    public async Task<ServiceSectionDto> UpsertAsync(ServiceSectionDto dto)
    {
        var section = await repo.GetWithItemsAsync();
        if (section is null)
        {
            section = new ServiceSection();
            await repo.AddAsync(section);
        }

        section.MainTitle = dto.MainTitle;
        section.MainDescription = dto.MainDescription;

        section.Items.Clear();
        foreach (var item in dto.Items)
        {
            section.Items.Add(new ServiceItem
            {
                ExternalId = item.Id,
                Icon = item.Icon,
                Title = item.Title,
                Description = item.Description,
            });
        }

        await repo.SaveChangesAsync();
        return MapSection(section);
    }

    private static ServiceSectionDto MapSection(ServiceSection s) => new(
        s.MainTitle,
        s.MainDescription,
        s.Items.Select(i => new ServiceItemDto(i.ExternalId, i.Icon, i.Title, i.Description)).ToList());
}
