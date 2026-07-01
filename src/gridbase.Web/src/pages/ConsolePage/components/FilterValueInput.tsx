import { ColumnSchema } from "common/data/ColumnSchema";
import { useState } from "react";
import { Badge, Button, Input } from "reactstrap"; 
import { RelationPicker } from "./RelationPicker";
import { numericTypes } from "./constants";

export const FilterValueInput: React.FC<{
    col: ColumnSchema | undefined;
    op: string;
    value: string;
    onChange: (v: string) => void;
}> = ({ col, op, value, onChange }) => {
    const type = col?.type ?? "Text";
    const [draft, setDraft] = useState("");

    if (op === "in") {
        const items = value ? value.split(",").map((s) => s.trim()).filter(Boolean) : [];
        const add = () => {
        const v = draft.trim();
        if (!v) return;
        if (!items.includes(v)) onChange([...items, v].join(","));
        setDraft("");
        };
        const remove = (v: string) => onChange(items.filter((x) => x !== v).join(","));
        return (
        <div>
            {items.length > 0 && (
            <div className="d-flex gap-1 mb-1 flex-wrap">
                {items.map((it) => (
                <Badge key={it} color="light" className="text-body border d-flex align-items-center gap-1 fw-normal">
                    {it}
                    <i className="ri-close-line" style={{ cursor: "pointer" }} onClick={() => remove(it)}></i>
                </Badge>
                ))}
            </div>
            )}
            <div className="d-flex gap-1">
            <Input
                bsSize="sm"
                value={draft}
                onChange={(e) => setDraft(e.target.value)}
                onKeyDown={(e) => { if (e.key === "Enter") { e.preventDefault(); add(); } }}
                placeholder="deger + Enter"
            />
            <Button color="light" size="sm" className="border" onClick={add}>
                <i className="ri-add-line"></i>
            </Button>
            </div>
        </div>
        );
    }

    if (col?.isForeign) {
        return (
        <div className="d-flex align-items-center gap-2">
            <Input bsSize="sm" value={value} onChange={(e) => onChange(e.target.value)} placeholder="id" style={{ maxWidth: 80 }} />
            <RelationPicker col={col} onPick={(id:any) => onChange(String(id))} />
        </div>
        );
    }

    if (numericTypes.includes(type)) {
        return <Input type="number" bsSize="sm" value={value} onChange={(e) => onChange(e.target.value)} placeholder="sayi" />;
    }
    if (type === "Date") {
        return <Input type="date" bsSize="sm" value={value} onChange={(e) => onChange(e.target.value)} />;
    }
    if (type === "DatetimeLocal") {
        return <Input type="datetime-local" bsSize="sm" value={value} onChange={(e) => onChange(e.target.value)} />;
    }
    if (type === "Checkbox" || type === "Switch") {
        return (
        <Input type="select" bsSize="sm" value={value} onChange={(e) => onChange(e.target.value)}>
            <option value="">— sec —</option>
            <option value="true">true</option>
            <option value="false">false</option>
        </Input>
        );
    }
    return <Input bsSize="sm" value={value} onChange={(e) => onChange(e.target.value)} placeholder="deger" />;
};