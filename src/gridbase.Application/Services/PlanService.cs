using gridbase.Application.Services.Interfaces;
using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;
using gridbase.DTO.DTOs;

namespace gridbase.Application.Services;

public class PlanService(IPlanRepository repo) : IPlanService
{
    public async Task<PlanSectionDto> GetAsync()
    {
        var section = await repo.GetWithItemsAsync();
        return section is null
            ? new PlanSectionDto("Fiyatlandırma", "Planlar yükleniyor", "Yıllık %20 İndirim", new())
            : MapSection(section);
    }

    public async Task<PlanSectionDto> UpsertAsync(PlanSectionDto dto)
    {
        var section = await repo.GetWithItemsAsync();
        if (section is null)
        {
            section = new PlanSection();
            await repo.AddAsync(section);
        }

        section.Title = dto.Title;
        section.Description = dto.Description;
        section.MonthlyDiscountLabel = dto.MonthlyDiscountLabel;

        section.Items.Clear();
        foreach (var item in dto.Items)
        {
            section.Items.Add(new PlanItem
            {
                ExternalId = item.Id,
                Name = item.Name,
                SubTitle = item.SubTitle,
                Icon = item.Icon,
                PriceMonthly = item.PriceMonthly,
                PriceAnnual = item.PriceAnnual,
                IsPopular = item.IsPopular,
                Features = item.Features.Select(f => new PlanFeature
                {
                    Text = f.Text,
                    IsIncluded = f.IsIncluded,
                }).ToList(),
            });
        }

        await repo.SaveChangesAsync();
        return MapSection(section);
    }

    private static PlanSectionDto MapSection(PlanSection s) => new(
        s.Title,
        s.Description,
        s.MonthlyDiscountLabel,
        s.Items.Select(i => new PlanItemDto(
            i.ExternalId, i.Name, i.SubTitle, i.Icon,
            i.PriceMonthly, i.PriceAnnual, i.IsPopular,
            i.Features.Select(f => new PlanFeatureDto(f.Text, f.IsIncluded)).ToList()
        )).ToList());
}

