import React, { useEffect, useMemo, useState } from "react";
import { useSearchParams } from "react-router-dom";
import { Row, Col } from "reactstrap";
import { useTablesAll, useGridbaseSchema } from "hooks/useGridBase";
import { TableFormModal } from "./components/TableFormModal";
import { AccessModal } from "./components/AccessModal";
import { LeftPanel } from "./components/LeftPanel";
import { RequestPanel } from "./components/RequestPanel"; 
import { ConsoleStyles } from "./components/ConsoleStyles"; 
import { EndpointDef, TableSchema } from "./components/console.types";
import { buildEndpoints, buildValidationTemplate, RELATION_BODY_TEMPLATE, toFieldType, VALIDATION_BODY_TEMPLATE } from "./components/constants";
import { useQueryBuilder } from "./hooks/useQueryBuilder";
import { useConsoleRequest } from "./hooks/useConsoleRequest";
import { ResponsePanel } from "./components/ResponsePanel";
import { ValidationModal } from "./components/ValidationModal";

const ConsolePage: React.FC = () => {
  const [searchParams] = useSearchParams();
  const tableFromUrl = searchParams.get("table") || "";

  const { data: tablesResp, isLoading: tablesLoading } = useTablesAll();
  const tables: any[] = useMemo(() => {
    const d: any = tablesResp;
    if (!d) return [];
    if (Array.isArray(d)) return d;
    if (Array.isArray(d.data)) return d.data;
    return [];
  }, [tablesResp]);

  const [selectedTable, setSelectedTable] = useState<string>(tableFromUrl);
  const [selectedEndpoint, setSelectedEndpoint] = useState<EndpointDef | null>(null);
  const [tableSearch, setTableSearch] = useState("");

  useEffect(() => { if (tableFromUrl) setSelectedTable(tableFromUrl); }, [tableFromUrl]);

  const endpoints = useMemo(
    () => (selectedTable ? buildEndpoints(selectedTable) : []),
    [selectedTable]
  );
  useEffect(() => {
    if (endpoints.length) setSelectedEndpoint(endpoints[0]);
    else setSelectedEndpoint(null);
  }, [selectedTable]); // eslint-disable-line

  const { data: schemaResp } = useGridbaseSchema(selectedTable);
  const schema: TableSchema | null = useMemo(() => {
    const d: any = schemaResp;
    if (!d) return null;
    if (d.columns) return d as TableSchema;
    if (d.data?.columns) return d.data as TableSchema;
    return null;
  }, [schemaResp]);

  // endpoint girişleri
  const [idValue, setIdValue] = useState("");
  const [colNameValue, setColNameValue] = useState("");
  const [bodyText, setBodyText] = useState("{}");

  const qb = useQueryBuilder(selectedEndpoint);
  const { respStatus, respBody, respTime, sending, send } = useConsoleRequest();

  useEffect(() => {
      if (!selectedEndpoint) return;
      qb.reset();
      if (selectedEndpoint.id === "relCreate") {
        setBodyText(RELATION_BODY_TEMPLATE);
      } else if (selectedEndpoint.id === "valSet") {
        setBodyText(VALIDATION_BODY_TEMPLATE);
      } else if (selectedEndpoint.needsBody && schema) {
        setBodyText(buildTemplate(schema));
      } else {
        setBodyText("{}");
      }
    }, [selectedEndpoint, schema]);

    useEffect(() => {
      if (selectedEndpoint?.id !== "valSet") return;
      if (!colNameValue || !schema) return;

      const col = schema.columns.find(
        (c: any) =>
          c.key?.toLowerCase() === colNameValue.toLowerCase() ||
          c.label?.toLowerCase() === colNameValue.toLowerCase()
      );
      if (!col) return;  

      const ft = toFieldType(col.type);
      setBodyText(buildValidationTemplate(ft));
    }, [colNameValue, selectedEndpoint, schema]);

  const buildTemplate = (s: TableSchema): string => {
    const obj: Record<string, any> = {};
    for (const c of s.columns) {
      if (c.key === "id") continue;
      obj[c.key] = c.default ?? (c.isMultiSelect ? [] : c.isForeign ? null : "");
    }
    return JSON.stringify(obj, null, 2);
  };

  const applyRelationPick = (colKey: string, id: number, multi: boolean) => {
    let obj: any;
    try { obj = JSON.parse(bodyText); } catch { obj = {}; }
    if (multi) {
      const arr = Array.isArray(obj[colKey]) ? obj[colKey] : [];
      if (!arr.includes(id)) arr.push(id);
      obj[colKey] = arr;
    } else { obj[colKey] = id; }
    setBodyText(JSON.stringify(obj, null, 2));
  };

  const buildUrl = (): string => {
    if (!selectedEndpoint) return "";
    let path = selectedEndpoint.path;
    if (selectedEndpoint.needsId) path = path.replace("{id}", idValue || "{id}");
    if (path.includes("{columnName}")) path = path.replace("{columnName}", colNameValue || "{columnName}");
    const q = selectedEndpoint.supportsQuery ? qb.rawQuery : "";
    return q ? `${path}?${q}` : path;
  };

  const onSend = () => {
    if (!selectedEndpoint) return;
    send(selectedEndpoint, buildUrl(), bodyText);
  };

  // modallar
  const [tableModalOpen, setTableModalOpen] = useState(false);
  const [tableModalMode, setTableModalMode] = useState<"create" | "edit">("create");
  const [editingTable, setEditingTable] = useState<any | null>(null);
  const [accessOpen, setAccessOpen] = useState(false);
  const [accessTable, setAccessTable] = useState<any | null>(null);

  const openCreateTable = () => { setTableModalMode("create"); setEditingTable(null); setTableModalOpen(true); };
  const openEditTable = (t: any) => { setTableModalMode("edit"); setEditingTable(t); setTableModalOpen(true); };
  const openAccess = (t: any) => { setAccessTable(t); setAccessOpen(true); };

  const [validationOpen, setValidationOpen] = useState(false);
  const [validationTable, setValidationTable] = useState<any | null>(null);
  const openValidation = (t: any) => { setValidationTable(t); setValidationOpen(true); };


  return (
    <div className="page-content">
      <ConsoleStyles />
      <div className="gb-console">
        <Row className="g-3">
          <Col xl={3} lg={4}>
            <LeftPanel
              tables={tables} tablesLoading={tablesLoading}
              tableSearch={tableSearch} setTableSearch={setTableSearch}
              selectedTable={selectedTable} setSelectedTable={setSelectedTable}
              endpoints={endpoints} selectedEndpoint={selectedEndpoint} setSelectedEndpoint={setSelectedEndpoint}
              onCreateTable={openCreateTable} onEditTable={openEditTable} onAccess={openAccess}
              selectedTableId={tables.find((t) => t.name === selectedTable)} 
              onValidation={openValidation}
            />
          </Col>

          <Col xl={5} lg={8}>
            <RequestPanel
              selectedEndpoint={selectedEndpoint} schema={schema} buildUrl={buildUrl}
              idValue={idValue} setIdValue={setIdValue}
              colNameValue={colNameValue} setColNameValue={setColNameValue}
              bodyText={bodyText} setBodyText={setBodyText}
              sending={sending} onSend={onSend}
              qb={qb} applyRelationPick={applyRelationPick}
            />
          </Col>

          <Col xl={4} lg={12}>
            <ResponsePanel respStatus={respStatus} respBody={respBody} respTime={respTime} />
          </Col>
        </Row>
      </div>

      <TableFormModal
        isOpen={tableModalOpen}
        toggle={() => setTableModalOpen(false)}
        mode={tableModalMode}
        initial={editingTable ? {
          id: editingTable.id, name: editingTable.name,
          viewType: editingTable.viewType, modalSize: editingTable.modalSize,
          pageSize: editingTable.pageSize, modalHeight: editingTable.modalHeight,
        } : undefined}
        onDone={(name) => setSelectedTable(name)}
      />

      {accessTable && (
        <AccessModal
          isOpen={accessOpen}
          toggle={() => setAccessOpen(false)}
          tableName={accessTable.name}
          tableId={accessTable.id}
        />
      )}
      {validationTable && (
        <ValidationModal
          isOpen={validationOpen}
          toggle={() => setValidationOpen(false)}
          tableName={validationTable.name}
        />
      )}
    </div>
  );
};

export default ConsolePage; 