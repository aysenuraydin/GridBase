export interface OverviewTableItem {
    id: number;
    name: string;
    rowCount: number;
    columnCount: number;
} 
export interface ProjectOverview {
    projectId: number;
    projectName: string;
    plan: string;
    tableCount: number;
    totalRows: number;
    fileCount: number;
    storageBytes: number;
    activeKeyCount: number;
    maxTables: number;
    maxStorageMb: number;
    recentTables: OverviewTableItem[];
    createdAt: string;
}