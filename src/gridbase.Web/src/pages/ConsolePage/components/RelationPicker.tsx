import { ColumnSchema } from "common/data/ColumnSchema";
import { api } from "helpers/backend_helper";
import { useState } from "react";
import { Dropdown, DropdownItem, DropdownMenu, DropdownToggle, Spinner } from "reactstrap";

export const RelationPicker: React.FC<{
    col: ColumnSchema;
    onPick: (id: number, multi: boolean) => void;
}> = ({ col, onPick }) => {
    const [open, setOpen] = useState(false);
    const [rows, setRows] = useState<any[]>([]);
    const [loading, setLoading] = useState(false);
    const [err, setErr] = useState("");

    const targetTable = col.relatedTable;

    const load = async () => {
        if (!targetTable) return;
        setLoading(true); setErr("");
        try {
        const res: any = await api.get(`/gridbase/${targetTable}`);
        const data = Array.isArray(res) ? res : res?.data ?? [];
        setRows(data);
        } catch (e: any) {
        setErr("Satirlar alinamadi");
        } finally {
        setLoading(false);
        }
    };

    const toggle = () => {
        const next = !open;
        setOpen(next);
        if (next && rows.length === 0) load();
    };

    const rowLabel = (r: any): string => {
        const entries = Object.entries(r).filter(([k]) => k !== "id");
        const firstText = entries.find(([, v]) => typeof v === "string" && v.trim().length > 0);
        const label = firstText ? String(firstText[1]) : "";
        return label ? `${label}  (#${r.id})` : `#${r.id}`;
    };

    return (
        <Dropdown isOpen={open} toggle={toggle} className="d-inline-block">
        <DropdownToggle caret size="sm" color="light" className="border">
            <i className="ri-links-line me-1 align-middle"></i>
            {col.isSelf ? "Ust kayit" : col.relatedTable}
            {col.isMultiSelect && <span className="text-muted ms-1">coklu</span>}
        </DropdownToggle>
        <DropdownMenu style={{ maxHeight: 280, overflowY: "auto", minWidth: 240 }}>
            <div className="dropdown-header text-uppercase" style={{ fontSize: 10, letterSpacing: ".04em" }}>
            {col.relatedTable} kaydi sec
            </div>
            {loading && <DropdownItem disabled><Spinner size="sm" className="me-2" /> Yukleniyor...</DropdownItem>}
            {err && <DropdownItem disabled className="text-danger">{err}</DropdownItem>}
            {!loading && !err && rows.length === 0 && (
            <DropdownItem disabled>Kayit yok</DropdownItem>
            )}
            {!loading && rows.map((r) => (
            <DropdownItem key={r.id} onClick={() => onPick(r.id, col.isMultiSelect)}>
                {rowLabel(r)}
            </DropdownItem>
            ))}
        </DropdownMenu>
        </Dropdown>
  );
};