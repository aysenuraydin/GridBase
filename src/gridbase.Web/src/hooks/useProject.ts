import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import {
    getMyProjects,
    getProjectQuota,
    getProjectById,
    createProject,
    updateProject,
    deleteProject,
    CreateProjectBody,
    UpdateProjectBody,
    getProjectOverview,
} from "helpers/backend_helper";   // <-- senin helper yolun

const PROJECTS_KEY = ["projects"];
const QUOTA_KEY = ["projects", "quota"];

// ── Listele ──
export const useMyProjects = () =>
    useQuery({
        queryKey: PROJECTS_KEY,
        queryFn: getMyProjects,
    });

// ── Kota ──
export const useProjectQuota = () =>
    useQuery({
        queryKey: QUOTA_KEY,
        queryFn: getProjectQuota,
    });

// ── Tek proje ──
export const useProject = (id?: number) =>
    useQuery({
        queryKey: ["projects", id],
        queryFn: () => getProjectById(id!),
        enabled: !!id,
    });

// ── Olustur ──
export const useCreateProject = () => {
    const qc = useQueryClient();
    return useMutation({
        mutationFn: (body: CreateProjectBody) => createProject(body),
        onSuccess: () => {
        qc.invalidateQueries({ queryKey: PROJECTS_KEY });
        qc.invalidateQueries({ queryKey: QUOTA_KEY });
        },
    });
};

// ── Guncelle ──
export const useUpdateProject = () => {
    const qc = useQueryClient();
    return useMutation({
        mutationFn: ({ id, body }: { id: number; body: UpdateProjectBody }) =>
        updateProject(id, body),
        onSuccess: () => qc.invalidateQueries({ queryKey: PROJECTS_KEY }),
    });
};

// ── Sil ──
export const useDeleteProject = () => {
    const qc = useQueryClient();
    return useMutation({
        mutationFn: (id: number) => deleteProject(id),
        onSuccess: () => {
        qc.invalidateQueries({ queryKey: PROJECTS_KEY });
        qc.invalidateQueries({ queryKey: QUOTA_KEY });
        },
    });
};

export const useProjectOverview = (projectId?: number) =>
    useQuery({
        queryKey: ["projects", projectId, "overview"],
        queryFn: () => getProjectOverview(projectId!),
        enabled: !!projectId,
    });