using gridbase.Domain.Entities;
using gridbase.Domain.Enums;


namespace gridbase.Application.Services.Interfaces;

public interface ITableAccessGuard
{
    bool IsAdmin { get; }

    void EnsureCanManageTables();
    void EnsureCanRead(Datatable table);
    void EnsureCanWrite(Datatable table);

    IEnumerable<TableRow> FilterOwned(Datatable table, IEnumerable<TableRow> rows);
    bool IsRowOwnedByCurrentUser(Datatable table, TableRow row);
}