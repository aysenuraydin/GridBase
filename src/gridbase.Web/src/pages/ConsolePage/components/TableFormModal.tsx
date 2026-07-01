import React, { useEffect, useState } from "react";
import {
    Button, Input, Label, Spinner,
    Modal, ModalHeader, ModalBody, ModalFooter,
} from "reactstrap";
import { useCreateTable, useUpdateTable } from "hooks/useGridBase";  
import { STORAGE_KEY } from "context/ProjectContext";

interface TableForm {
    name: string;
    viewType: string;
    modalSize: string;
    pageSize: string;
    modalHeight: string;
}

const emptyForm: TableForm = { name: "", viewType: "List", modalSize: "Md", pageSize: "10", modalHeight: "" };

export const TableFormModal: React.FC<{
    isOpen: boolean;
    toggle: () => void;
    mode: "create" | "edit";
    initial?: { id?: number; name: string; viewType?: string; modalSize?: string; pageSize?: number; modalHeight?: number };
    onDone?: (name: string) => void;
}> = ({ isOpen, toggle, mode, initial, onDone }) => {
    const createMut = useCreateTable();
    const updateMut = useUpdateTable();

    const [form, setForm] = useState<{name:string}>(emptyForm);
    const [err, setErr] = useState("");

    useEffect(() => {
        if (!isOpen) return;
        setErr("");
        if (mode === "edit" && initial) {
        setForm({
            name: initial.name ?? "", 
        });
        } else {
        setForm(emptyForm);
        }
    }, [isOpen, mode, initial]);

    const set = (patch: Partial<TableForm>) => setForm((f) => ({ ...f, ...patch }));

    const submit = async () => {
        if (!form.name.trim()) { setErr("Tablo adı zorunludur."); return; }
        setErr("");

        const payload = {
            projectId: Number(localStorage.getItem(STORAGE_KEY)),
            name: form.name.trim(), 
        };

        try {
            if (mode === "create") {
                await createMut.mutateAsync(payload);
            } else if (initial?.id != null) {
                await updateMut.mutateAsync({ id: initial.id, payload });
            }
            onDone?.(payload.name);
            toggle();
        } catch (e: any) {
            const msg = e?.response?.data?.message || e?.response?.data || e?.message || "İşlem başarısız.";
            setErr(typeof msg === "string" ? msg : "İşlem başarısız.");
        }
    };

    const busy = createMut.isPending || updateMut.isPending;

    return (
        <Modal isOpen={isOpen} toggle={toggle} centered>
        <ModalHeader toggle={toggle}>
            {mode === "create" ? "Yeni tablo" : "Tabloyu düzenle"}
        </ModalHeader>
        <ModalBody>
            <div className="mb-3">
            <Label className="form-label">Tablo adı <span className="text-danger">*</span></Label>
            <Input value={form.name} onChange={(e) => set({ name: e.target.value })}
                placeholder="name" onKeyDown={(e) => { if (e.key === "Enter") submit(); }} autoFocus />
            </div>

            {err && <div className="text-danger small mt-3"><i className="ri-error-warning-line me-1"></i>{err}</div>}
        </ModalBody>
        <ModalFooter>
            <Button color="light" onClick={toggle}>Vazgeç</Button>
            <Button color="primary" onClick={submit} disabled={busy}>
            {busy ? <><Spinner size="sm" className="me-1" /> Kaydediliyor</> : (mode === "create" ? "Oluştur" : "Kaydet")}
            </Button>
        </ModalFooter>
        </Modal>
    );
};