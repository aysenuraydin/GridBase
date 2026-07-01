import React from "react";
import { useParams, Navigate } from "react-router-dom";
import { useTenantContext } from "context/TenantContext";
import { useGetTable } from "hooks/useDatatables";

export const TableFeatureGuard = ({ tableId, children }: { tableId: number; children: React.ReactNode }) => {
    const { config: tenantConfig } = useTenantContext();
    const { data: datatable, isLoading } = useGetTable(tableId);
    
    if (isLoading) {
        return (
            <div className="page-content">
                <div className="text-center py-5">Yükleniyor...</div>
            </div>
        );
    }
    
    const name: string = datatable?.data?.name ?? "";
    
    if (!name) {
        return <Navigate to="/dashboard" replace />;
    } 
    return <Navigate to="/dashboard" replace />; 
}; 