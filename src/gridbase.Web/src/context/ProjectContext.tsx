import React, { createContext, useContext, useEffect, useState, useCallback } from "react";

export const STORAGE_KEY = "gb_selected_project";

interface ProjectContextValue {
    selectedProjectId: number | null;
    selectProject: (id: number | null) => void;
    clearProject: () => void;
}

const ProjectContext = createContext<ProjectContextValue>({
    selectedProjectId: null,
    selectProject: () => {},
    clearProject: () => {},
});

export const ProjectProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const [selectedProjectId, setSelectedProjectId] = useState<number | null>(() => {
        try {
        const raw = localStorage.getItem(STORAGE_KEY);
        return raw ? Number(raw) : null;
        } catch {
        return null;
        }
    });
    
    useEffect(() => {
        try {
        if (selectedProjectId == null) localStorage.removeItem(STORAGE_KEY);
        else localStorage.setItem(STORAGE_KEY, String(selectedProjectId));
        } catch { /* yoksay */ }
    }, [selectedProjectId]);

    const selectProject = useCallback((id: number | null) => setSelectedProjectId(id), []);
    const clearProject = useCallback(() => setSelectedProjectId(null), []);

    return (
        <ProjectContext.Provider value={{ selectedProjectId, selectProject, clearProject }}>
        {children}
        </ProjectContext.Provider>
    );
};

export const useProjectContext = () => useContext(ProjectContext);

export const getSelectedProjectId = (): number | null => {
    try {
        const raw = localStorage.getItem(STORAGE_KEY);
        return raw ? Number(raw) : null;
    } catch {
        return null;
    }
};