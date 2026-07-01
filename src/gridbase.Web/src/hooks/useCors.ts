import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { getCorsOrigins, addCorsOrigin, removeCorsOrigin } from "helpers/backend_helper";
    
const KEY = (p: number) => ["projects", p, "cors"];
    
export const useCorsOrigins = (projectId?: number) =>
    useQuery({ queryKey: KEY(projectId!), queryFn: () => getCorsOrigins(projectId!), enabled: !!projectId });
    
export const useAddCorsOrigin = (projectId: number) => {
    const qc = useQueryClient();
    return useMutation({
        mutationFn: (origin: string) => addCorsOrigin(projectId, origin),
        onSuccess: () => qc.invalidateQueries({ queryKey: KEY(projectId) }),
    });
};
    
export const useRemoveCorsOrigin = (projectId: number) => {
    const qc = useQueryClient();
    return useMutation({
        mutationFn: (originId: number) => removeCorsOrigin(projectId, originId),
        onSuccess: () => qc.invalidateQueries({ queryKey: KEY(projectId) }),
    });
};