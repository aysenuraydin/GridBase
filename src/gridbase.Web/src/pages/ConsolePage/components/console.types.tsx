export interface ColumnSchema {
  key: string;
  label: string;
  type: string;
  default: any;
  isForeign: boolean;
  isSelf: boolean;
  isMultiSelect: boolean;
  relatedTable: string | null;
}

export interface TableSchema {
  table: string;
  columns: ColumnSchema[];
}

export type Method = "GET" | "POST" | "PUT" | "PATCH" | "DELETE";

export interface EndpointDef {
  id: string;
  method: Method;
  path: string;
  label: string;
  needsId: boolean;
  needsBody: boolean;
  supportsQuery: boolean;
}

export interface FilterRow {
  col: string;
  op: string;
  val: string;
}