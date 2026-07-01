import React from "react";
import { useNavigate } from "react-router-dom";
import { Card, CardBody, Button } from "reactstrap";
import { useProjectContext } from "context/ProjectContext";   

const RequireProject: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const navigate = useNavigate();
    const { selectedProjectId } = useProjectContext();

    if (selectedProjectId == null) {
        return (
        <div className="page-content">
            <div className="d-flex align-items-center justify-content-center" style={{ minHeight: "60vh" }}>
            <Card className="shadow-none border text-center" style={{ maxWidth: 420 }}>
                <CardBody className="p-4">
                <div className="mb-3">
                    <i className="ri-stack-line display-5 text-primary opacity-75"></i>
                </div>
                <h5 className="fw-semibold mb-2">Önce bir proje seç</h5>
                <p className="text-muted mb-4">
                    Tablolar, Console ve Storage seçili projeye göre çalışır.
                    Devam etmek için bir proje seç ya da yeni proje oluştur.
                </p>
                <Button color="primary" onClick={() => navigate("/projects")}>
                    <i className="ri-folders-line me-1"></i> Projelerime git
                </Button>
                </CardBody>
            </Card>
            </div>
        </div>
        );
    }

    return <>{children}</>;
};

export default RequireProject;