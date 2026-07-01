import React, { useState } from "react";
import {
    Button, Spinner, Modal, ModalHeader, ModalBody, ModalFooter, Badge,
} from "reactstrap";
import { useEmptyColumns, usePruneEmptyColumns } from "hooks/useGridBase"; 

export const PruneColumnsTool: React.FC<{
    tableId?: number;
    tableName: string;
}> = ({ tableId, tableName }) => {
    const [open, setOpen] = useState(false);

    // boş kolonlar — sadece modal açıkken çekilsin
    const { data: emptyResp, isLoading, refetch } =
        useEmptyColumns<any>(open && tableId ? tableId : 0);

    const pruneMut = usePruneEmptyColumns(tableId ?? 0);

    const emptyCols: any[] = (() => {
        const d: any = emptyResp;
        const arr = d?.data ?? d ?? [];
        return Array.isArray(arr) ? arr : [];
    })();

    const onPrune = async () => {
        if (emptyCols.length === 0) return;
        try {
        await pruneMut.mutateAsync(undefined);  
        setOpen(false);
        } catch {
        }
    };

    return (
        <>
        <button type="button"
            className="btn btn-sm btn-soft-secondary w-100 d-flex align-items-center justify-content-center gap-1"
            title="Boş kolonları temizle"
            onClick={() => { setOpen(true); }}
            disabled={!tableId}>
            <i className="ri-eraser-line"></i>
            <span className="small">Boş kolonları temizle</span>
        </button>

        <Modal isOpen={open} toggle={() => setOpen(false)} centered>
            <ModalHeader toggle={() => setOpen(false)}>
            <i className="ri-eraser-line me-2"></i>
            Boş kolonlar — <code className="text-primary">{tableName}</code>
            </ModalHeader>
            <ModalBody>
            {isLoading ? (
                <div className="text-center text-muted py-4">
                <Spinner size="sm" className="me-2" /> Boş kolonlar taranıyor
                </div>
            ) : emptyCols.length === 0 ? (
                <div className="text-center text-muted py-4">
                <i className="ri-checkbox-circle-line display-6 text-success opacity-50 d-block mb-2"></i>
                Hiç boş kolon yok. Tablo temiz.
                </div>
            ) : (
                <>
                <p className="text-muted small mb-2">
                    Aşağıdaki <b>{emptyCols.length}</b> kolonda hiç veri yok. Silmek
                    geri alınamaz.
                </p>
                <div className="d-flex flex-wrap gap-2">
                    {emptyCols.map((c) => (
                    <Badge key={c.id ?? c.name} color="light"
                        className="text-body border fw-normal d-flex align-items-center gap-1">
                        <i className="ri-table-line opacity-50"></i>
                        {c.label ?? c.name ?? c.key}
                    </Badge>
                    ))}
                </div>
                </>
            )}
            </ModalBody>
            <ModalFooter>
            <Button color="light" onClick={() => setOpen(false)}>Kapat</Button>
            {emptyCols.length > 0 && (
                <Button color="danger" onClick={onPrune} disabled={pruneMut.isPending}>
                {pruneMut.isPending
                    ? <><Spinner size="sm" className="me-1" /> Siliniyor</>
                    : <><i className="ri-delete-bin-line me-1"></i> {emptyCols.length} kolonu sil</>}
                </Button>
            )}
            </ModalFooter>
        </Modal>
        </>
    );
};