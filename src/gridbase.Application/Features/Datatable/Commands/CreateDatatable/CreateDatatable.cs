using MediatR;
using gridbase.Application.Common.Models;
using gridbase.Domain.Common;
using gridbase.Domain.Enums;

namespace gridbase.Application.Features.Datatables.Commands.CreateDatatable;

public class CreateDatatableCommand : IRequest<Result<long>>
{
    public long ProjectId { get; set; }
    public string Name { get; set; }
    public ModalSizeType? ModalSize { get; set; }
    public TableViewType? ViewType { get; set; }
    public int? PageSize { get; set; }
}
public class CreateTableCommandHandler : IRequestHandler<CreateDatatableCommand, Result<long>>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateTableCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Result<long>> Handle(CreateDatatableCommand request, CancellationToken cancellationToken)
    {
        // 🔒 Hidden. Akış: ad doğrula/normalize → benzersizlik (case-insensitive) →
        //   Datatable.Create(projectId, ...) → kaydet (id için commit) → Result.
        throw new NotImplementedException("Source available on request.");
    }
}