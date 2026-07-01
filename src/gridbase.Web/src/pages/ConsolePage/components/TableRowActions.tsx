import { useDeleteTable } from "hooks/useGridBase";
import { Spinner } from "reactstrap";


export const TableRowActions: React.FC<{
    table: any;
    onEdit: (t: any) => void;
    onDeleted: (name: string) => void;
}> = ({ table, onEdit, onDeleted }) => {
    const deleteMut = useDeleteTable();

    const del = (e: React.MouseEvent) => {
        e.stopPropagation();
        if (!window.confirm(`"${table.name}" tablosu silinsin mi? Tüm satırları ve kolonları silinir.`)) return;
        deleteMut.mutate({ id: table.id, hard: true }, {
        onSuccess: () => onDeleted(table.name),
        });
    }; 

    return (
        <>
            <button type="button" className="btn btn-sm p-0 px-1 text-muted border-0 bg-transparent"
                title="Düzenle" onClick={(e) => { e.stopPropagation(); onEdit(table); }}>
                <i className="ri-edit-line"></i>
            </button>
            <button type="button" className="btn btn-sm p-0 px-1 text-danger border-0 bg-transparent"
                title="Sil" onClick={del} disabled={deleteMut.isPending}>
                {deleteMut.isPending ? <Spinner size="sm" /> : <i className="ri-delete-bin-line"></i>}
            </button>
        </>
    );
};