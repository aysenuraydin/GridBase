import { useProjectContext } from "context/ProjectContext";
import { useDeleteProject, useProject, useUpdateProject } from "hooks/useProject";
import CorsSettings from "pages/ProjectSettingsPage/CorsSettings";
import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import {
    Button, Input, Label, Spinner,
    Modal, ModalHeader, ModalBody, ModalFooter,
    Container,
} from "reactstrap";

const ProjectSettingsPage: React.FC = () => {
    const navigate = useNavigate();
    const { selectedProjectId, clearProject } = useProjectContext();
    const projectId = selectedProjectId ?? 0;

    const { data: project, isLoading } = useProject(projectId || undefined);
    const updateMut = useUpdateProject();
    const deleteMut = useDeleteProject();

    const [name, setName] = useState("");
    const [description, setDescription] = useState("");
    const [saved, setSaved] = useState(false);
    const [err, setErr] = useState("");

    const [delOpen, setDelOpen] = useState(false);
    const [delConfirm, setDelConfirm] = useState("");

    useEffect(() => {
        const p: any = project;
        const data = p?.data ?? p;
        if (data) {
        setName(data.name ?? "");
        setDescription(data.description ?? "");
        }
    }, [project]);

    if (!selectedProjectId) {
        return (
        <div className="page-content">
            <div className="text-center text-muted py-5">
            <i className="ri-settings-3-line display-5 opacity-25 d-block mb-2"></i>
            Önce bir proje seç (üst bardan).
            </div>
        </div>
        );
    }

    const projData: any = (project as any)?.data ?? project;

    const save = async () => {
        if (!name.trim()) { setErr("Proje adı zorunludur."); return; }
        setErr(""); setSaved(false);
        try {
        await updateMut.mutateAsync({ id: projectId, body: { name: name.trim(), description: description.trim() } });
        setSaved(true);
        setTimeout(() => setSaved(false), 2000);
        } catch (e: any) {
        const msg = e?.response?.data?.message || e?.response?.data || e?.message || "Kaydedilemedi.";
        setErr(typeof msg === "string" ? msg : "Kaydedilemedi.");
        }
    };

    const confirmDelete = async () => {
        await deleteMut.mutateAsync(projectId);
        clearProject();
        navigate("/projects");
    };

    const canDelete = delConfirm.trim() === (projData?.name ?? "");

    return (
        <div className="page-content" style={{ userSelect: "none" }}>   
            <Container fluid>
                <style>{` 
                    .gb-settings .gb-stat {
                    flex: 1; background: var(--vz-light); border-radius: 10px; padding: 14px 16px;
                    }
                    .gb-settings .gb-stat-num { font-size: 22px; font-weight: 600; line-height: 1.1; }
                    .gb-settings .gb-stat-num.sm { font-size: 15px; }
                    .gb-settings .gb-section {
                    background: var(--vz-card-bg); border: 1px solid var(--vz-border-color);
                    border-radius: 12px; padding: 1.25rem; margin-bottom: 12px;
                    }
                    .gb-settings .gb-section-head {
                    display: flex; align-items: center; gap: 10px; margin-bottom: 14px;
                    }
                    .gb-settings .gb-danger {
                    border: 1px solid rgba(var(--vz-danger-rgb), .4);
                    background: rgba(var(--vz-danger-rgb), .04);
                    border-radius: 12px; padding: 1.25rem;
                    }
                `}</style>

                <div className="gb-settings">
                    <div className="mb-4">
                    <h4 className="mb-1 fw-semibold">Proje ayarları</h4>
                    <p className="text-muted mb-0">Bu projenin bilgilerini düzenle, erişimi yönet ya da projeyi sil.</p>
                    </div>

                    {isLoading ? (
                    <div className="text-center text-muted py-4"><Spinner size="sm" className="me-2" /> Yükleniyor</div>
                    ) : (
                    <>
                        {/* ── ÖZET METRİKLER ── */}
                        <div className="d-flex gap-2 mb-3">
                        <div className="gb-stat">
                            <div className="gb-stat-num">{projData?.tableCount ?? 0}</div>
                            <small className="text-muted">tablo</small>
                        </div>
                        <div className="gb-stat">
                            <div className={`gb-stat-num sm ${projData?.plan === 1 ? "text-primary" : ""}`}>
                            {projData?.plan === 1 ? "Pro" : "Free"}
                            </div>
                            <small className="text-muted">plan</small>
                        </div>
                        <div className="gb-stat">
                            <div className="gb-stat-num sm">
                            {projData?.createdAt ? new Date(projData.createdAt).toLocaleDateString("tr-TR", { day: "2-digit", month: "short" }) : "—"}
                            </div>
                            <small className="text-muted">oluşturma</small>
                        </div>
                        </div>

                        {/* ── GENEL ── */}
                        <div className="gb-section">
                        <div className="gb-section-head">
                            <i className="ri-settings-4-line text-muted" style={{ fontSize: 18 }}></i>
                            <span className="fw-semibold">Genel</span>
                        </div>
                        <div className="mb-3">
                            <Label className="form-label">Proje adı</Label>
                            <Input value={name} onChange={(e) => setName(e.target.value)} />
                        </div>
                        <div className="mb-3">
                            <Label className="form-label">Açıklama</Label>
                            <Input type="textarea" rows={3} value={description} onChange={(e) => setDescription(e.target.value)} />
                        </div>
                        {err && <div className="text-danger small mb-2"><i className="ri-error-warning-line me-1"></i>{err}</div>}
                        <div className="d-flex align-items-center gap-2">
                            <Button color="primary" onClick={save} disabled={updateMut.isPending}>
                            {updateMut.isPending ? <><Spinner size="sm" className="me-1" /> Kaydediliyor</> : "Kaydet"}
                            </Button>
                            {saved && <span className="text-success small"><i className="ri-check-line me-1"></i>Kaydedildi</span>}
                        </div>
                        </div>

                        {/* ── CORS ── */}
                        <CorsSettings projectId={projectId} />

                        {/* ── TEHLİKELİ BÖLGE ── */}
                        <div className="gb-danger">
                        <div className="d-flex align-items-center justify-content-between flex-wrap gap-2">
                            <div>
                            <div className="fw-medium text-danger">
                                <i className="ri-alert-line me-1"></i>Projeyi sil
                            </div>
                            <small className="text-danger" style={{ opacity: .85 }}>
                                Bu projenin tüm tabloları, satırları, dosyaları ve API anahtarları
                                kalıcı olarak silinir. Geri alınamaz.
                            </small>
                            </div>
                            <Button color="danger" outline onClick={() => { setDelConfirm(""); setDelOpen(true); }} style={{ whiteSpace: "nowrap" }}>
                            <i className="ri-delete-bin-line me-1"></i> Projeyi sil
                            </Button>
                        </div>
                        </div>
                    </>
                    )}
                </div>

                <Modal isOpen={delOpen} toggle={() => setDelOpen(false)} centered>
                    <ModalHeader toggle={() => setDelOpen(false)} className="text-danger">Projeyi sil</ModalHeader>
                    <ModalBody>
                    <div className="alert alert-danger d-flex align-items-start gap-2">
                        <i className="ri-alert-line fs-18"></i>
                        <div className="small">
                        Bu işlem <strong>geri alınamaz</strong>. Tablolar, satırlar, dosyalar
                        ve API anahtarları kalıcı silinir.
                        </div>
                    </div>
                    <Label className="form-label">
                        Onaylamak için proje adını yaz: <strong>{projData?.name}</strong>
                    </Label>
                    <Input value={delConfirm} onChange={(e) => setDelConfirm(e.target.value)} placeholder={projData?.name} />
                    </ModalBody>
                    <ModalFooter>
                    <Button color="light" onClick={() => setDelOpen(false)}>Vazgeç</Button>
                    <Button color="danger" onClick={confirmDelete} disabled={!canDelete || deleteMut.isPending}>
                        {deleteMut.isPending ? <><Spinner size="sm" className="me-1" /> Siliniyor</> : "Kalıcı olarak sil"}
                    </Button>
                    </ModalFooter>
                </Modal>
            </Container>
        </div>
    );
};

export default ProjectSettingsPage;