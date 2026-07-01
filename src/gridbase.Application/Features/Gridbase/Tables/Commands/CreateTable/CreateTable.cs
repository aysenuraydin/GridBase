using MediatR;
using gridbase.Application.Common.Models;
using gridbase.Application.Services.Interfaces;
using gridbase.Domain.Enums;
using gridbase.DTO.DTOs;

namespace gridbase.Application.Features.Tables.Commands.CreateTable;

public class CreateTableCommand : IRequest<Result<TableSummaryResponse>>
{
    public string Name { get; set; } = null!;
    public ModalSizeType? ModalSize { get; set; }
    public TableViewType? ViewType { get; set; }
    public int? PageSize { get; set; }
    public int? ModalHeight { get; set; }
}
public class CreateTableCommandHandler : IRequestHandler<CreateTableCommand, Result<TableSummaryResponse>>
{
    private readonly IGridBaseService _service;
    public CreateTableCommandHandler(IGridBaseService service) => _service = service;

    public async Task<Result<TableSummaryResponse>> Handle(CreateTableCommand request, CancellationToken cancellationToken)
    {
        // 🔒 Hidden. Akış: request → CreateTableRequest DTO → motor servisinde tablo
        //   oluştur → Success.
        throw new NotImplementedException("Source available on request.");
    }
}