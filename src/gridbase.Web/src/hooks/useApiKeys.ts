
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import {
    getProjectKeys, createProjectKey, revokeProjectKey,
    ApiKeyType,
} from "helpers/backend_helper";

const KEYS = (projectId: number) => ["projects", projectId, "keys"];

export const useProjectKeys = (projectId?: number) =>
    useQuery({
        queryKey: KEYS(projectId!),
        queryFn: () => getProjectKeys(projectId!),
        enabled: !!projectId,
    });

export const useCreateProjectKey = (projectId: number) => {
    const qc = useQueryClient();
    return useMutation({
        mutationFn: (body: { keyType: ApiKeyType; name?: string }) =>
        createProjectKey(projectId, body),
        onSuccess: () => qc.invalidateQueries({ queryKey: KEYS(projectId) }),
    });
};

export const useRevokeProjectKey = (projectId: number) => {
    const qc = useQueryClient();
    return useMutation({
        mutationFn: (keyId: number) => revokeProjectKey(projectId, keyId),
        onSuccess: () => qc.invalidateQueries({ queryKey: KEYS(projectId) }),
    });
};