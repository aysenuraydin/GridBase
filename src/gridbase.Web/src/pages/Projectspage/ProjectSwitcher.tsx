import React from "react";
import { useNavigate } from "react-router-dom";
import {
  UncontrolledDropdown, DropdownToggle, DropdownMenu, DropdownItem, Spinner,
} from "reactstrap";
import { useProjectContext } from "context/ProjectContext";    
import { useMyProjects } from "hooks/useProject";
const ProjectSwitcher: React.FC = () => {
    const navigate = useNavigate();
    const { selectedProjectId, selectProject } = useProjectContext();
    const { data, isLoading } = useMyProjects();

    const projects: any[] = Array.isArray(data) ? data : (data as any)?.data ?? [];
    const selected = projects.find((p) => p.id === selectedProjectId) ?? null;

    return (
        <UncontrolledDropdown className="w-100">
        <DropdownToggle
            tag="button"
            type="button"
            className="btn w-100 d-flex align-items-center justify-content-between text-start"
            style={{
                background: "rgba(255,255,255,.08)",
                borderRadius: 8,
                padding: "8px 12px",
            }}
        >
            <span className="d-flex align-items-center text-truncate">
            <i className="ri-stack-line me-2" style={{ color: "#a8b3ff" }}></i>
            <span className="text-truncate" style={{ color: "#e9ecef", fontSize: 13, fontWeight: 500 }}>
                {selected ? selected.name : "Proje seç"}
            </span>
            </span>
            <i className="ri-arrow-down-s-line" style={{ color: "#a8b3ff" }}></i>
        </DropdownToggle>

        <DropdownMenu className="dropdown-menu-start" style={{ minWidth: 220 }}>
            <div className="dropdown-header text-uppercase" style={{ fontSize: 10, letterSpacing: ".04em" }}>
            Projelerim
            </div>

            {isLoading && (
            <DropdownItem disabled><Spinner size="sm" className="me-2" /> Yukleniyor</DropdownItem>
            )}

            {!isLoading && projects.length === 0 && (
            <DropdownItem disabled>Henuz proje yok</DropdownItem>
            )}

            {projects.map((p) => (
            <DropdownItem
                key={p.id}
                active={p.id === selectedProjectId}
                onClick={() => selectProject(p.id)}
                className="d-flex align-items-center"
            >
                <i className={`ri-stack-line me-2 ${p.id === selectedProjectId ? "text-primary" : "text-muted"}`}></i>
                <span className="text-truncate flex-grow-1">{p.name}</span>
                {p.id === selectedProjectId && <i className="ri-check-line ms-1 text-primary"></i>}
            </DropdownItem>
            ))}

            <div className="dropdown-divider"></div>
            <DropdownItem onClick={() => navigate("/projects")}>
            <i className="ri-folders-line me-2 text-muted"></i> Tum projeler
            </DropdownItem>
        </DropdownMenu>
        </UncontrolledDropdown>
    );
};

export default ProjectSwitcher;