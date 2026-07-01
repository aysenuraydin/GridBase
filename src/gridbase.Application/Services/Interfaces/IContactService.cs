using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Application.Services.Interfaces;

public interface IContactService
{
    Task<ContactConfigDto> GetAsync();
    Task<ContactConfigDto> UpsertAsync(ContactConfigDto dto);
}