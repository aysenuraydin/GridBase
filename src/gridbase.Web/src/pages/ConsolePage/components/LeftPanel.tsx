import React from "react";
import { Link } from "react-router-dom";
import { Card, CardBody, CardHeader, Input, Button, Badge, Spinner } from "reactstrap";
import { SectionLabel } from "./SectionLabel";
import { EndpointDef } from "./console.types";
import { methodColor } from "./constants";
import { useDeleteTable } from "hooks/useGridBase";
import { PruneColumnsTool } from "./PruneColumnsTool"; 
import { ColumnManagerTool } from "./ColumnManagerTool";


export const LeftPanel: React.FC<{
    tables: any[];
    tablesLoading: boolean;
    tableSearch: string;
    setTableSearch: (v: string) => void;
    selectedTable: string;
    setSelectedTable: (v: string) => void;
    endpoints: EndpointDef[];
    selectedEndpoint: EndpointDef | null;
    setSelectedEndpoint: (e: EndpointDef) => void;
    onCreateTable: () => void;
    onEditTable: (t: any) => void;
    onAccess: (t: any) => void;
    selectedTableId:number;
    onValidation: (t: any) => void;
}> = ({
    tables, tablesLoading, tableSearch, setTableSearch,
    selectedTable, setSelectedTable, endpoints, selectedEndpoint, setSelectedEndpoint,
    onCreateTable, onEditTable, onAccess, selectedTableId, onValidation
}) => {
    const filteredTables = tables.filter(
        (t) => !tableSearch || (t.name ?? "").toLowerCase().includes(tableSearch.toLowerCase())
    );

    const deleteMut = useDeleteTable();

    const del = (e: React.MouseEvent, table: any) => {
        e.stopPropagation();
        if (!window.confirm(`"${table.name}" tablosu silinsin mi? Tüm satırları ve kolonları silinir.`)) return;
            deleteMut.mutate({ id: table.id, hard: true }, {
            onSuccess: () => { if (selectedTable === table.name) setSelectedTable(""); },
        });
    };

    return (
        <Card className="h-100 shadow-none border">
        <style>{`
            .gb-console .gb-tools {
            display: inline-flex; align-items: center; gap: 1px;
            padding: 2px; border-radius: 8px;
            background: var(--vz-card-bg); border: 1px solid var(--vz-border-color);
            opacity: 0; transform: translateX(4px);
            transition: opacity .15s ease, transform .15s ease;
            }
            .gb-console .gb-nav-item:hover .gb-tools { opacity: 1; transform: translateX(0); }
            .gb-console .gb-tool {
            width: 26px; height: 26px; display: inline-flex; align-items: center; justify-content: center;
            border: 0; background: transparent; border-radius: 5px;
            color: var(--vz-secondary-color, #878a99); font-size: 14px; line-height: 1;
            cursor: pointer; transition: background-color .12s ease, color .12s ease; text-decoration: none;
            }
            .gb-console .gb-tool:hover { background: var(--vz-light); color: var(--vz-dark); }
            .gb-console .gb-tool:disabled { opacity: .5; cursor: default; }
            .gb-console .gb-tool-danger:hover { background: rgba(var(--vz-danger-rgb), .12); color: var(--vz-danger); }
            .gb-console .gb-tool-accent { color: var(--vz-primary); }
            .gb-console .gb-tool-accent:hover { background: rgba(var(--vz-primary-rgb), .12); color: var(--vz-primary); }
            .gb-console .gb-tool-sep { width: 1px; height: 16px; background: var(--vz-border-color); margin: 0 2px; }
        `}</style>

        <CardHeader className="bg-transparent border-bottom d-flex align-items-center py-3">
            <div className="avatar-xs flex-shrink-0 me-2">
            <span className="avatar-title bg-primary-subtle text-primary rounded fs-15">
                <i className="ri-terminal-box-line"></i>
            </span>
            </div>
            <div>
            <h6 className="mb-0 fw-semibold">API Console</h6>
            <small className="text-muted">Tablolarini test et</small>
            </div>
        </CardHeader>

        <CardBody className="p-2" style={{ maxHeight: "76vh", overflowY: "auto" }}>
            <div className="position-relative mb-2">
            <i className="ri-search-line position-absolute text-muted" style={{ left: 10, top: 8, fontSize: 14 }}></i>
            <Input bsSize="sm" value={tableSearch} onChange={(e) => setTableSearch(e.target.value)}
                placeholder="Tablo ara" style={{ paddingLeft: 30 }} />
            </div>

            <div className="d-flex align-items-center justify-content-between px-1 mb-2">
            <SectionLabel className="mb-0">
                Tablolar {!tablesLoading && <span className="text-muted">· {filteredTables.length}</span>}
            </SectionLabel>
            <button type="button" className="btn btn-sm btn-soft-primary py-0 px-2"
                title="Yeni tablo" onClick={onCreateTable}>
                <i className="ri-add-line"></i>
            </button>
            </div>

            {tablesLoading && (
            <div className="text-muted px-2 py-3 text-center">
                <Spinner size="sm" className="me-1" /> Yukleniyor
            </div>
            )}

            <div>
            {filteredTables.map((t) => (
                <button key={t.id ?? t.name} type="button"
                onClick={() => setSelectedTable(t.name)}
                className={`gb-nav-item w-100 text-start d-flex align-items-center justify-content-between ${selectedTable === t.name ? "active" : ""}`}>
                <span className="d-flex align-items-center text-truncate">
                    <i className="ri-table-line me-2 opacity-75"></i>
                    <span className="text-truncate">{t.name}</span>
                </span>

                {/* ── ARAÇ ÇUBUĞU (yeni tasarım) ── */}
                <span className="gb-tools" onClick={(e) => e.stopPropagation()}>
                    <button type="button" className="gb-tool" title="Erişim kuralları"
                    onClick={(e) => { e.stopPropagation(); onAccess(t); }}>
                    <i className="ri-shield-keyhole-line"></i>
                    </button>
                    <button type="button" className="gb-tool" title="Doğrulama kuralları"
                    onClick={(e) => { e.stopPropagation(); onValidation(t); }}>
                    <i className="ri-shield-check-line"></i>
                    </button>
                    <button type="button" className="gb-tool" title="Düzenle"
                    onClick={(e) => { e.stopPropagation(); onEditTable(t); }}>
                    <i className="ri-edit-line"></i>
                    </button>
                    <button type="button" className="gb-tool gb-tool-danger" title="Sil"
                    onClick={(e) => del(e, t)} disabled={deleteMut.isPending}>
                    {deleteMut.isPending ? <Spinner size="sm" /> : <i className="ri-delete-bin-line"></i>}
                    </button>
                    <span className="gb-tool-sep"></span>
                    <Link to={`/datatable/${t.id}`} className="gb-tool gb-tool-accent"
                    title="Tablo görünümünde aç" onClick={(e) => e.stopPropagation()}>
                    <i className="ri-external-link-line"></i>
                    </Link>
                </span>
                </button>
            ))}
            {!tablesLoading && filteredTables.length === 0 && (
                <div className="text-muted text-center py-3 small">Eslesen tablo yok</div>
            )}
            </div>

            {selectedTable && (
            <>
                <SectionLabel className="px-1 mt-3">Araçlar</SectionLabel>
                <div className="px-1 mb-2">
                    <ColumnManagerTool tableName={selectedTable} />
                    <PruneColumnsTool tableId={selectedTableId} tableName={selectedTable} />
                </div>

                <SectionLabel className="px-1 mt-3">Uc Noktalar</SectionLabel>
                <div>
                {endpoints.map((ep) => (
                    <button key={ep.id} type="button"
                    onClick={() => setSelectedEndpoint(ep)}
                    className={`gb-nav-item w-100 text-start d-flex align-items-center gap-2 ${selectedEndpoint?.id === ep.id ? "active" : ""}`}>
                    <Badge color={methodColor[ep.method]} className="gb-method-pill fs-10">{ep.method}</Badge>
                    <span className="gb-ep-label text-truncate">{ep.label}</span>
                    </button>
                ))}
                </div>
            </>
            )}
        </CardBody>
        </Card>
    );
};