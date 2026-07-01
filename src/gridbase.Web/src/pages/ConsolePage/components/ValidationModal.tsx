import React, { useState, useEffect, useMemo } from "react";
import {
    Button, Input, Label, Spinner, Badge,
    Modal, ModalHeader, ModalBody, ModalFooter,
    Accordion, AccordionItem, AccordionHeader, AccordionBody,
} from "reactstrap";
import { useGridbaseSchema, useGetColumnValidation, useSetColumnValidation } from "hooks/useGridBase";

type RuleMeta = {
    rule: string;
    label: string;
    hasValue: boolean;
    valuePlaceholder?: string;
    forTypes?: string[];
};

const ALL_RULES: RuleMeta[] = [
    { rule: "required",      label: "Zorunlu",            hasValue: false },
    { rule: "unique",        label: "Benzersiz",          hasValue: false },
    { rule: "email",         label: "E-posta",            hasValue: false, forTypes: ["text"] },
    { rule: "url",           label: "URL",                hasValue: false, forTypes: ["text"] },
    { rule: "minLength",     label: "Min uzunluk",        hasValue: true, valuePlaceholder: "3",  forTypes: ["text"] },
    { rule: "maxLength",     label: "Max uzunluk",        hasValue: true, valuePlaceholder: "50", forTypes: ["text"] },
    { rule: "length",        label: "Tam uzunluk",        hasValue: true, valuePlaceholder: "10", forTypes: ["text"] },
    { rule: "pattern",       label: "Desen (regex)",      hasValue: true, valuePlaceholder: "^[a-z0-9-]+$", forTypes: ["text"] },
    { rule: "matches",       label: "Eşleşme (regex)",    hasValue: true, valuePlaceholder: "^...$", forTypes: ["text"] },
    { rule: "allowedValues", label: "İzinli değerler",    hasValue: true, valuePlaceholder: "active,passive,draft" },
    { rule: "min",           label: "Min değer",          hasValue: true, valuePlaceholder: "0",   forTypes: ["number"] },
    { rule: "max",           label: "Max değer",          hasValue: true, valuePlaceholder: "100", forTypes: ["number"] },
    { rule: "positive",      label: "Pozitif",            hasValue: false, forTypes: ["number"] },
    { rule: "negative",      label: "Negatif",            hasValue: false, forTypes: ["number"] },
    { rule: "integer",       label: "Tam sayı",           hasValue: false, forTypes: ["number"] },
    { rule: "trim",          label: "Boşlukları kırp",    hasValue: false, forTypes: ["text"] },
];

const FIELD_TYPES = ["text", "number", "boolean", "date", "array", "mixed"];

const toFieldType = (schemaType: string): string => {
    const t = (schemaType || "").toLowerCase();
    if (["number", "range", "ratings", "progress"].includes(t)) return "number";
    if (["boolean", "checkbox", "switch"].includes(t)) return "boolean";
    if (["date", "datetime", "datetimelocal"].includes(t)) return "date";
    if (["badges", "dropfiles", "multipledate", "multipletime"].includes(t)) return "array";
    return "text";
};

interface RuleState {
    rule: string;
    active: boolean;
    value: string;
    message: string;
}

export const ValidationModal: React.FC<{
    isOpen: boolean;
    toggle: () => void;
    tableName: string;
    onDone?: () => void;
}> = ({ isOpen, toggle, tableName, onDone }) => {
    const { data: schemaResp } = useGridbaseSchema(tableName);
    const setMut = useSetColumnValidation(tableName);

    const [column, setColumn] = useState("");
    const [fieldType, setFieldType] = useState("text");
    const [rules, setRules] = useState<RuleState[]>([]);
    const [mode, setMode] = useState<"visual" | "json">("visual");
    const [jsonText, setJsonText] = useState("{}");
    const [err, setErr] = useState("");
    const [openAcc, setOpenAcc] = useState<string>("recommended"); // açık panel

    const columns: any[] = useMemo(() => {
        const d: any = schemaResp;
        const cols = d?.columns ?? d?.data?.columns ?? [];
        return Array.isArray(cols) ? cols.filter((c: any) => !c.isForeign) : [];
    }, [schemaResp]);

    const { data: existResp, isLoading: existLoading } = useGetColumnValidation(
        tableName, column, isOpen && !!column
    );

    useEffect(() => {
        if (!isOpen) return;
        setErr("");
        setMode("visual");
        setOpenAcc("recommended");
        if (!column && columns.length) setColumn(columns[0].key);
    }, [isOpen]); // eslint-disable-line

    useEffect(() => {
        if (!column) return;
        const col = columns.find((c) => c.key === column);
        if (col) setFieldType(toFieldType(col.type));
    }, [column, columns]);

    useEffect(() => {
        const a = existResp?.data ?? existResp ?? null;
        if (a && Array.isArray(a.rules)) {
            if (a.type) setFieldType(String(a.type).toLowerCase());
            setRules(
                a.rules.map((r: any) => ({
                    rule: String(r.rule),
                    active: r.isActive !== false,
                    value: r.value ?? "",
                    message: r.message ?? "",
                }))
            );
        } else {
            setRules([]);
        }
    }, [existResp]);

    const findRule = (ruleName: string) => rules.find((r) => r.rule === ruleName);

    const toggleRule = (ruleName: string) => {
        setRules((prev) => {
            const exists = prev.find((r) => r.rule === ruleName);
            if (exists) return prev.filter((r) => r.rule !== ruleName);
            return [...prev, { rule: ruleName, active: true, value: "", message: "" }];
        });
    };

    const patchRule = (ruleName: string, patch: Partial<RuleState>) => {
        setRules((prev) => prev.map((r) => (r.rule === ruleName ? { ...r, ...patch } : r)));
    };

    const buildJson = (): string => {
        const payload = {
            type: fieldType,
            rules: rules.map((r) => ({
                rule: r.rule,
                isActive: r.active,
                value: r.value || null,
                message: r.message || null,
            })),
        };
        return JSON.stringify(payload, null, 2);
    };

    const switchToJson = () => { setJsonText(buildJson()); setMode("json"); };
    const switchToVisual = () => {
        try {
            const obj = JSON.parse(jsonText);
            if (obj.type) setFieldType(String(obj.type).toLowerCase());
            if (Array.isArray(obj.rules)) {
                setRules(obj.rules.map((r: any) => ({
                    rule: String(r.rule),
                    active: r.isActive !== false,
                    value: r.value ?? "",
                    message: r.message ?? "",
                })));
            }
            setErr("");
        } catch {
            setErr("JSON geçersiz — görsel moda dönülemedi.");
            return;
        }
        setMode("visual");
    };

    const submit = async () => {
        setErr("");
        if (!column) { setErr("Kolon seç."); return; }

        let payload: { type: string; rules: any[] };
        if (mode === "json") {
            try {
                const obj = JSON.parse(jsonText);
                payload = { type: obj.type ?? fieldType, rules: obj.rules ?? [] };
            } catch {
                setErr("JSON geçersiz."); return;
            }
        } else {
            payload = {
                type: fieldType,
                rules: rules.map((r) => ({
                    rule: r.rule,
                    isActive: r.active,
                    value: r.value || null,
                    message: r.message || null,
                })),
            };
        }

        try {
            await setMut.mutateAsync({ columnName: column, ...payload });
            onDone?.();
            toggle();
        } catch (e: any) {
            const msg = e?.response?.data?.message || e?.response?.data || e?.message || "Kaydedilemedi.";
            setErr(typeof msg === "string" ? msg : "Kaydedilemedi.");
        }
    };

    const sortedRules = useMemo(() => {
        const recommended = ALL_RULES.filter((r) => !r.forTypes || r.forTypes.includes(fieldType));
        const others = ALL_RULES.filter((r) => r.forTypes && !r.forTypes.includes(fieldType));
        return { recommended, others };
    }, [fieldType]);

    // accordion açık/kapa
    const accToggle = (id: string) => setOpenAcc((cur) => (cur === id ? "" : id));

    // bir kural listesinde kaç tanesi aktif (accordion başlığı rozeti için)
    const activeIn = (list: RuleMeta[]) =>
        list.filter((m) => rules.some((r) => r.rule === m.rule)).length;

    const busy = setMut.isPending;
    const activeCount = rules.length;

    const RuleRow: React.FC<{ meta: RuleMeta }> = ({ meta }) => {
        const state = findRule(meta.rule);
        const on = !!state;
        return (
            <div className={`p-2 rounded mb-2 ${on ? "border border-primary border-2 bg-primary-subtle" : "border"}`}>
                <div className="d-flex align-items-center gap-2" style={{ cursor: "pointer" }}
                     onClick={() => toggleRule(meta.rule)}>
                    <div className="form-check form-switch m-0">
                        <Input type="switch" role="switch" checked={on}
                            onChange={() => toggleRule(meta.rule)}
                            onClick={(e) => e.stopPropagation()} />
                    </div>
                    <span className="fw-medium" style={{ fontSize: 13 }}>{meta.label}</span>
                    <code className="ms-auto text-muted bg-light rounded px-2 py-1" style={{ fontSize: 11 }}>{meta.rule}</code>
                </div>
                {on && (
                    <div className="mt-2 ps-5">
                        {meta.hasValue && (
                            <Input bsSize="sm" className="mb-1"
                                placeholder={meta.valuePlaceholder || "değer"}
                                value={state!.value}
                                onChange={(e) => patchRule(meta.rule, { value: e.target.value })} />
                        )}
                        <Input bsSize="sm"
                            placeholder="Özel hata mesajı (opsiyonel)"
                            value={state!.message}
                            onChange={(e) => patchRule(meta.rule, { message: e.target.value })} />
                    </div>
                )}
            </div>
        );
    };

    const recActive = activeIn(sortedRules.recommended);
    const othActive = activeIn(sortedRules.others);

    return (
        <Modal isOpen={isOpen} toggle={toggle} centered size="lg" scrollable>
            <ModalHeader toggle={toggle}>
                <div className="d-flex align-items-center gap-2">
                    <span className="avatar-title bg-primary-subtle text-primary rounded fs-18"
                          style={{ width: 34, height: 34 }}>
                        <i className="ri-shield-check-line" />
                    </span>
                    <div>
                        <div className="fw-semibold" style={{ fontSize: 15 }}>Doğrulama kuralları</div>
                        <div className="text-muted" style={{ fontSize: 12 }}>{tableName} tablosu</div>
                    </div>
                </div>
            </ModalHeader>

            <ModalBody>
                <div className="d-flex gap-2 mb-3">
                    <div style={{ flex: 1.4 }}>
                        <Label className="form-label small mb-1">Kolon</Label>
                        <Input type="select" bsSize="sm" value={column} onChange={(e) => setColumn(e.target.value)}>
                            <option value="">— kolon seç —</option>
                            {columns.map((c) => <option key={c.key} value={c.key}>{c.label}</option>)}
                        </Input>
                    </div>
                    <div style={{ flex: 1 }}>
                        <Label className="form-label small mb-1">Tip</Label>
                        <Input type="select" bsSize="sm" value={fieldType} onChange={(e) => setFieldType(e.target.value)}>
                            {FIELD_TYPES.map((t) => <option key={t} value={t}>{t}</option>)}
                        </Input>
                    </div>
                </div>

                <div className="d-flex align-items-center mb-3">
                    <div className="btn-group btn-group-sm" role="group">
                        <Button color={mode === "visual" ? "primary" : "light"} size="sm"
                            onClick={() => (mode === "json" ? switchToVisual() : setMode("visual"))}>
                            <i className="ri-list-check me-1" /> Görsel
                        </Button>
                        <Button color={mode === "json" ? "primary" : "light"} size="sm"
                            onClick={() => (mode === "visual" ? switchToJson() : setMode("json"))}>
                            <i className="ri-code-line me-1" /> JSON
                        </Button>
                    </div>
                    {mode === "visual" && activeCount > 0 && (
                        <Badge color="primary" className="bg-primary-subtle text-primary ms-auto">
                            {activeCount} aktif kural
                        </Badge>
                    )}
                </div>

                {existLoading && column && (
                    <div className="text-muted small mb-2"><Spinner size="sm" className="me-1" /> Mevcut kurallar yükleniyor</div>
                )}

                {mode === "visual" ? (
                    <Accordion open={openAcc} toggle={accToggle} flush>
                        <AccordionItem>
                            <AccordionHeader targetId="recommended">
                                <i className="ri-sparkling-2-line text-primary me-2" />
                                <span className="fw-medium">{fieldType} için önerilen</span>
                                {recActive > 0 && (
                                    <Badge color="primary" className="bg-primary-subtle text-primary ms-2">{recActive}</Badge>
                                )}
                            </AccordionHeader>
                            <AccordionBody accordionId="recommended">
                                {sortedRules.recommended.map((m) => <RuleRow key={m.rule} meta={m} />)}
                            </AccordionBody>
                        </AccordionItem>

                        {sortedRules.others.length > 0 && (
                            <AccordionItem>
                                <AccordionHeader targetId="others">
                                    <i className="ri-more-2-line text-muted me-2" />
                                    <span className="fw-medium">Diğer kurallar</span>
                                    {othActive > 0 && (
                                        <Badge color="primary" className="bg-primary-subtle text-primary ms-2">{othActive}</Badge>
                                    )}
                                </AccordionHeader>
                                <AccordionBody accordionId="others">
                                    {sortedRules.others.map((m) => <RuleRow key={m.rule} meta={m} />)}
                                </AccordionBody>
                            </AccordionItem>
                        )}
                    </Accordion>
                ) : (
                    <div>
                        <Label className="form-label small">Kural JSON'ı (elle düzenle)</Label>
                        <Input type="textarea" rows={12} style={{ fontFamily: "monospace", fontSize: 12 }}
                            value={jsonText} onChange={(e) => setJsonText(e.target.value)} />
                        <div className="text-muted small mt-1">
                            Format: {`{ "type": "text", "rules": [ { "rule": "required", "value": null, "message": "..." } ] }`}
                        </div>
                    </div>
                )}

                {err && <div className="text-danger small mt-3"><i className="ri-error-warning-line me-1" />{err}</div>}
            </ModalBody>

            <ModalFooter>
                <Button color="light" onClick={toggle}>Vazgeç</Button>
                <Button color="primary" onClick={submit} disabled={busy || !column}>
                    {busy ? <><Spinner size="sm" className="me-1" /> Kaydediliyor</>
                        : <><i className="ri-check-line me-1" /> Kuralları kaydet</>}
                </Button>
            </ModalFooter>
        </Modal>
    );
};