

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

export interface TableSchemaResponse {
    table: string;
    columns: ColumnSchema[];
}