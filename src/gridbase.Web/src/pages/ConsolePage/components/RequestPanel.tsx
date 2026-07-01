import React from "react";
import Editor from "@monaco-editor/react";
import { Card, CardBody, CardHeader, Row, Col, Input, Button, Badge, Label, Spinner } from "reactstrap";
import { SectionLabel } from "./SectionLabel";
import { RelationPicker } from "./RelationPicker";
import { SelectCheckbox } from "./SelectCheckbox";
import { FilterValueInput } from "./FilterValueInput"; 
import { EndpointDef, TableSchema } from "./console.types";
import { methodColor, opsForType, toCamelTable } from "./constants";
import { AutoComplete } from "antd";

export const RequestPanel: React.FC<{
    selectedEndpoint: EndpointDef | null;
    schema: TableSchema | null;
    buildUrl: () => string;
    idValue: string; setIdValue: (v: string) => void;
    colNameValue: string; setColNameValue: (v: string) => void;
    bodyText: string; setBodyText: (v: string) => void;
    sending: boolean;
    onSend: () => void;
    qb: any;   
    applyRelationPick: (colKey: string, id: number, multi: boolean) =>
        void;
}> = ({
    selectedEndpoint, schema, buildUrl,
    idValue, setIdValue, colNameValue, setColNameValue,
    bodyText, setBodyText, sending, onSend, qb, applyRelationPick,
}) => {
    if (!selectedEndpoint) {
        return (
        <Card className="h-100 shadow-none border">
            <CardBody className="d-flex flex-column align-items-center justify-content-center text-center text-muted py-5">
            <i className="ri-cursor-line display-5 opacity-25 mb-3"></i>
            <h6 className="text-muted">Bir tablo ve uc nokta sec</h6>
            <p className="small mb-0">Soldaki listeden basla, istegini burada kur.</p>
            </CardBody>
        </Card>
        );
    }

    const foreignCols = schema?.columns.filter((c) => c.isForeign) ?? [];

    return (
        <Card className="h-100 shadow-none border">
        <CardHeader className="bg-transparent border-bottom py-3">
            <div className="gb-url-bar d-flex align-items-center gap-2">
            <Badge color={methodColor[selectedEndpoint.method]} className="gb-method-pill flex-shrink-0">
                {selectedEndpoint.method}
            </Badge>
            <code className="flex-grow-1 text-truncate bg-transparent p-0">/api{buildUrl()}</code>
            <Button color="primary" size="sm" onClick={onSend} disabled={sending} className="flex-shrink-0">
                {sending
                ? <><Spinner size="sm" className="me-1" /> Gönderiliyor</>
                : <><i className="ri-send-plane-fill me-1"></i> Gönder</>}
            </Button>
            </div>
        </CardHeader>

        <CardBody style={{ maxHeight: "70vh", overflowY: "auto" }}>
            {selectedEndpoint.needsId && (
            <div className="gb-section">
                <Label className="form-label fw-semibold">Kayit id</Label>
                <Input value={idValue} onChange={(e) => setIdValue(e.target.value)} placeholder="orn: 1" />
            </div>
            )}

            {selectedEndpoint.path.includes("{columnName}") && (
            <div className="gb-section">
                <Label className="form-label fw-semibold">Kolon adi</Label>
                <AutoComplete
                    style={{ width: "100%" }}
                    value={colNameValue}
                    placeholder="orn: name (seç ya da yaz)"
                    options={(schema?.columns ?? [])
                        .filter((c) => !c.isForeign)
                        .map((c) => ({ value: c.key, label: c.label }))}
                    filterOption={(input, option) =>
                        (option?.value ?? "").toLowerCase().includes(input.toLowerCase()) ||
                        (String(option?.label) ?? "").toLowerCase().includes(input.toLowerCase())
                    }
                    onChange={(val) => setColNameValue(val)}
                    onSelect={(val) => setColNameValue(val)}
                    allowClear
                />
                <div className="text-muted small mt-1">
                    <i className="ri-list-check-2 me-1"></i>
                    Listeden seç, ara ya da elle yaz
                </div>
            </div>
            )}

            {selectedEndpoint.supportsQuery && (
            <>
                {/* Filtreler */}
                <div className="gb-section">
                <SectionLabel>Filtreler</SectionLabel>
                {qb.filters.length === 0 && <div className="text-muted small mb-2">Henuz filtre yok.</div>}
                {qb.filters.map((f: any, i: number) => {
                    const colType = schema?.columns.find((c) => c.key === f.col)?.type ?? "Text";
                    const ops = opsForType(colType);
                    const needsVal = !["isnull", "isnotnull"].includes(f.op);
                    return (
                    <Row className="g-2 mb-2 align-items-center" key={i}>
                        <Col>
                        <Input type="select" bsSize="sm" value={f.col} onChange={(e) => qb.updateFilter(i, { col: e.target.value })}>
                            {schema?.columns.map((c) => <option key={c.key} value={c.key}>{c.label}</option>)}
                        </Input>
                        </Col>
                        <Col xs="auto">
                        <Input type="select" bsSize="sm" value={f.op} onChange={(e) => qb.updateFilter(i, { op: e.target.value })} style={{ width: 130 }}>
                            {ops.map((o: string) => <option key={o} value={o}>{o}</option>)}
                        </Input>
                        </Col>
                        {needsVal && (
                        <Col>
                            <FilterValueInput
                            col={schema?.columns.find((c) => c.key === f.col)}
                            op={f.op} value={f.val}
                            onChange={(v: any) => qb.updateFilter(i, { val: v })}
                            />
                        </Col>
                        )}
                        <Col xs="auto">
                        <Button color="ghost-danger" size="sm" onClick={() => qb.removeFilter(i)} title="Kaldir">
                            <i className="ri-delete-bin-line"></i>
                        </Button>
                        </Col>
                    </Row>
                    );
                })}
                <Button color="soft-primary" size="sm" onClick={() => qb.addFilter(schema?.columns[0]?.key ?? "")}>
                    <i className="ri-add-line me-1"></i> Filtre ekle
                </Button>
                </div>

                {/* Sıralama */}
                <div className="gb-section">
                <SectionLabel>Siralama</SectionLabel>
                <Row className="g-2">
                    <Col>
                    <Input type="select" bsSize="sm" value={qb.sortCol} onChange={(e) => qb.setSortCol(e.target.value)}>
                        <option value="">— yok —</option>
                        {schema?.columns.map((c) => <option key={c.key} value={c.key}>{c.label}</option>)}
                    </Input>
                    </Col>
                    <Col xs="auto">
                    <Input type="select" bsSize="sm" value={qb.sortDir} onChange={(e) => qb.setSortDir(e.target.value as any)} style={{ width: 110 }}>
                        <option value="asc">artan</option>
                        <option value="desc">azalan</option>
                    </Input>
                    </Col>
                </Row>
                </div>

                {/* Alanlar (select) */}
                <div className="gb-section">
                <div className="d-flex align-items-center justify-content-between mb-2">
                    <SectionLabel className="mb-0">Alanlar</SectionLabel>
                    <div className="btn-group btn-group-sm" role="group">
                    <Button color={qb.selectMode === "include" ? "primary" : "light"} size="sm" onClick={() => qb.setSelectMode("include")}>Sadece bunlar</Button>
                    <Button color={qb.selectMode === "exclude" ? "danger" : "light"} size="sm" onClick={() => qb.setSelectMode("exclude")}>Bunlar haric</Button>
                    </div>
                </div>
                <div className="d-flex flex-wrap gap-3">
                    {schema?.columns.map((c) => (
                    <SelectCheckbox key={c.key} id={`sel-${c.key}`} label={c.label}
                        checked={qb.selectCols.includes(c.key)} exclude={qb.selectMode === "exclude"}
                        onChange={() => qb.toggleSelect(c.key)} />
                    ))}
                </div>
                {qb.selectCols.length > 0 && (
                    <div className="text-muted small mt-2">
                    {qb.selectMode === "include" ? "id + secilen alanlar doner." : "Secilenler haric tum alanlar doner (id her zaman gelir)."}
                    </div>
                )}
                </div>

                {/* Arama */}
                <div className="gb-section">
                <SectionLabel>Arama</SectionLabel>
                <Input value={qb.searchText} onChange={(e) => qb.setSearchText(e.target.value)} placeholder="Metin alanlarinda ara" />
                {qb.searchText && (
                    <>
                    <div className="text-muted small mt-2 mb-1">Hangi alanlarda? (bos = tum metin alanlari)</div>
                    <div className="d-flex flex-wrap gap-3">
                        {schema?.columns.filter((c) => !c.isForeign).map((c) => (
                        <div className="form-check" key={`sf-${c.key}`}>
                            <Input type="checkbox" className="form-check-input" id={`sf-${c.key}`}
                            checked={qb.searchFields.includes(c.key)}
                            onChange={() => qb.setSearchFields((s: string[]) =>
                                s.includes(c.key) ? s.filter((k) => k !== c.key) : [...s, c.key])} />
                            <Label className="form-check-label user-select-none" htmlFor={`sf-${c.key}`}>{c.label}</Label>
                        </div>
                        ))}
                    </div>
                    </>
                )}
                </div>

                {/* İlişkiler (expand) */}
                {foreignCols.length > 0 && (
                <div className="gb-section">
                    <SectionLabel>İlişkiler (expand)</SectionLabel>
                    <div className="text-muted small mb-2">
                        Seçilen ilişkiler tek istekte dolu nesne olarak gelir
                        (id korunur + nesne eklenir).
                    </div>
                    <div className="d-flex flex-wrap gap-3">
                        {foreignCols.filter((c) => !c.isSelf).map((c) => {
                            const expandName = toCamelTable(c.relatedTable);
                            return (
                                <div className="form-check" key={`exp-${c.key}`}>
                                    <Input type="checkbox" className="form-check-input"
                                        id={`exp-${c.key}`}
                                        checked={qb.expandCols.includes(expandName)}
                                        onChange={() => qb.toggleExpand(expandName)} />
                                    <Label className="form-check-label user-select-none" htmlFor={`exp-${c.key}`}>
                                        {c.label}
                                        <code className="text-muted ms-1" style={{ fontSize: 11 }}>
                                            → {expandName}
                                        </code>
                                    </Label>
                                </div>
                            );
                        })}
                    </div>
                </div>
                )}

                {/* Sayfalama */}
                {selectedEndpoint.id === "paged" && (
                <div className="gb-section">
                    <SectionLabel>Sayfalama</SectionLabel>
                    <Row className="g-2">
                    <Col xs="auto">
                        <Label className="form-label small text-muted mb-1">Sayfa</Label>
                        <Input value={qb.page} onChange={(e) => qb.setPage(e.target.value)} style={{ width: 110 }} placeholder="1" />
                    </Col>
                    <Col xs="auto">
                        <Label className="form-label small text-muted mb-1">Boyut</Label>
                        <Input value={qb.size} onChange={(e) => qb.setSize(e.target.value)} style={{ width: 130 }} placeholder="varsayilan" />
                    </Col>
                    </Row>
                    <div className="text-muted small mt-1">Boyut bos birakilirsa tablonun kendi sayfa boyutu kullanilir.</div>
                </div>
                )}

                {/* Query string */}
                <div className="gb-section">
                <SectionLabel>Query String</SectionLabel>
                <Input value={qb.rawQuery} onChange={(e) => qb.onRawQueryChange(e.target.value)}
                    placeholder="filter=...&sort=..." className="font-monospace text-primary" style={{ fontSize: 12.5 }} />
                {foreignCols.length > 0 && (
                    <div className="text-muted small mt-2">
                    <i className="ri-links-line me-1"></i>
                    Iliskiler: {foreignCols.map((c) => `${c.label}${c.isSelf ? " (ust kayit)" : ` -> ${c.relatedTable}`}`).join(", ")}
                    </div>
                )}
                </div>
            </>
            )}

            {/* Body */}
            {selectedEndpoint.needsBody && (
            <>
                {foreignCols.length > 0 && (
                <div className="gb-section">
                    <SectionLabel>Iliski sec</SectionLabel>
                    <div className="d-flex flex-wrap gap-2">
                    {foreignCols.map((c) => (
                        <div key={c.key} className="d-flex align-items-center gap-1">
                        <span className="small text-muted">{c.label}</span>
                        <RelationPicker col={c} onPick={(id, multi) => applyRelationPick(c.key, id, multi)} />
                        </div>
                    ))}
                    </div>
                </div>
                )}

                <div className="gb-section">
                <SectionLabel>Govde (JSON)</SectionLabel>
                <div className="border rounded overflow-hidden">
                    <Editor height="520px" defaultLanguage="json" value={bodyText}
                    onChange={(v) => setBodyText(v ?? "{}")}
                    options={{ minimap: { enabled: false }, fontSize: 13, scrollBeyondLastLine: false, padding: { top: 10 } }} />
                </div>
                </div>
            </>
            )}
        </CardBody>
        </Card>
    );
};