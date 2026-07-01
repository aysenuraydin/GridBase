using gridbase.Application.Common.Services;
using gridbase.Application.Services.Interfaces;
using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Application.Services;

public class DatatableService : BaseService<Datatable>, IDatatableService
{
    public DatatableService(IRepository<Datatable, long> repository) : base(repository)
    {
    }
}