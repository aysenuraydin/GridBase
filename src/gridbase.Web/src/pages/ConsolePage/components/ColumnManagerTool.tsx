import React, { useState } from "react";
import {
    Button, Spinner, Modal, ModalHeader, ModalBody, ModalFooter, Badge,
} from "reactstrap";
import { useGridbaseSchema, useDeleteColumn } from "hooks/useGridBase"; 

export const ColumnManagerTool: React.FC<{
    tableName: string;
}> = ({ tableName }) => {
    const [open, setOpen] = useState(false);
    const [deletingKey, setDeletingKey] = useState<string | null>(null);

    const { data: schemaResp, isLoading } = useGridbaseSchema(open ? tableName : "");
    const deleteCol = useDeleteColumn(tableName);

    const columns: any[] = (() => {
        const d: any = schemaResp;
        const cols = d?.columns ?? d?.data?.columns ?? [];
        return Array.isArray(cols) ? cols.filter((c: any) => c.key !== "id") : [];
    })();

    const onDelete = async (col: any) => {
        const name = col.key ?? col.name;
            setDeletingKey(name);
        try {
        await deleteCol.mutateAsync({ columnName: name, hard: false });
        } catch { 
        } finally {
            setDeletingKey(null);
        }
    };

    return (
        <>
        <button type="button"
            className="btn btn-sm btn-soft-secondary w-100 d-flex align-items-center justify-content-center gap-1"
            title="Kolonları yönet"
            onClick={() => setOpen(true)}
            disabled={!tableName}>
            <i className="ri-table-line"></i>
            <span className="small">Kolonları yönet</span>
        </button>

        <Modal isOpen={open} toggle={() => setOpen(false)} centered>
            <ModalHeader toggle={() => setOpen(false)}>
            <i className="ri-table-line me-2"></i>
            Kolonlar — <code className="text-primary">{tableName}</code>
            </ModalHeader>
            <ModalBody>
            {isLoading ? (
                <div className="text-center text-muted py-4">
                <Spinner size="sm" className="me-2" /> Kolonlar yükleniyor
                </div>
            ) : columns.length === 0 ? (
                <div className="text-center text-muted py-4">
                Silinebilir kolon yok.
                </div>
            ) : (
                <>
                <p className="text-muted small mb-3">
                    Silmek istediğin kolonun yanındaki çöp ikonuna bas. Kolon silinince
                    o kolondaki tüm veriler de gider.
                </p>
                <div className="d-flex flex-column gap-1">
                    {columns.map((c) => {
                    const name = c.key ?? c.name;
                    const busy = deletingKey === name;
                    return (
                        <div key={name}
                        className="d-flex align-items-center justify-content-between p-2 rounded border">
                        <div className="d-flex align-items-center gap-2 text-truncate">
                            <i className="ri-table-line text-muted opacity-75"></i>
                            <span className="text-truncate">
                            {c.label ?? name}
                            <span className="text-muted ms-2 small">{c.type}</span>
                            </span>
                            {c.isForeign && (
                            <Badge color="info" className="fw-normal">ilişki</Badge>
                            )}
                        </div>
                        <button type="button"
                            className="btn btn-sm p-0 px-2 text-danger border-0 bg-transparent"
                            title="Bu kolonu sil"
                            onClick={() => onDelete(c)}
                            disabled={busy || deleteCol.isPending}>
                            {busy ? <Spinner size="sm" /> : <i className="ri-delete-bin-line"></i>}
                        </button>
                        </div>
                    );
                    })}
                </div>
                </>
            )}
            </ModalBody>
            <ModalFooter>
            <Button color="light" onClick={() => setOpen(false)}>Kapat</Button>
            </ModalFooter>
        </Modal>
        </>
    );
};