import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import {
  CardBody, Button, Input, Label, Spinner,
  Modal, ModalHeader, ModalBody, ModalFooter,
} from "reactstrap";
import {
  useMyProjects, useProjectQuota, useCreateProject, useDeleteProject,
} from "hooks/useProject";
import { ProjectListItem } from "helpers/backend_helper";
import { useProjectContext } from "context/ProjectContext";
import { PopConfirm } from "components/Common/PopConfirm";
import { ModalType } from "common/enums/ModalType";
import { toast } from "react-toastify";

export const ProjectsPage: React.FC = () => {
    const navigate = useNavigate();
    const { selectProject } = useProjectContext();

    const { data: projects, isLoading } = useMyProjects();
    const { data: quota } = useProjectQuota();
    const createMut = useCreateProject();
    const deleteMut = useDeleteProject();

    const [modalOpen, setModalOpen] = useState(false);
    const [name, setName] = useState("");
    const [description, setDescription] = useState("");
    const [err, setErr] = useState("");

    const list: ProjectListItem[] = Array.isArray(projects) ? projects : (projects as any)?.data ?? [];
    const canCreate = quota ? quota.canCreate : true;
    const used = quota?.used ?? list.length;
    const max = quota?.max ?? 2;
    const remaining = Math.max(0, max - used);

    const openModal = () => { setName(""); setDescription(""); setErr(""); setModalOpen(true); };

    const submit = async () => {
        if (!name.trim()) { setErr("Proje adı zorunludur."); return; }
        setErr("");
        try {
        await createMut.mutateAsync({ name: name.trim(), description: description.trim() || undefined });
        setModalOpen(false);
        } catch (e: any) {
        const msg = e?.response?.data?.message || e?.response?.data || e?.message || "Proje oluşturulamadı.";
        setErr(typeof msg === "string" ? msg : "Proje oluşturulamadı.");
        }
    };

    const openProject = (p: ProjectListItem) => { selectProject(p.id); navigate("/datatables"); };

    const onDelete = (e: React.MouseEvent, p: ProjectListItem) => {
        e.stopPropagation();
        deleteMut.mutate(p.id);
    };

    return (
        <div className="page-content">
        <style>{`
            .gb-projects { --gb-r: 14px; }
            .gb-projects .gb-grid {
            display: grid; grid-template-columns: repeat(auto-fill, minmax(240px, 1fr)); gap: 16px;
            }
            .gb-projects .gb-card {
            background: var(--vz-card-bg); border: 1px solid var(--vz-border-color);
            border-radius: var(--gb-r); padding: 1.1rem 1.25rem; cursor: pointer;
            transition: box-shadow .18s ease, transform .18s ease, border-color .18s ease;
            display: flex; flex-direction: column; height: 100%;
            }
            .gb-projects .gb-card:hover {
            box-shadow: 0 10px 30px rgba(0,0,0,.09); transform: translateY(-3px);
            border-color: var(--vz-primary);
            }
            .gb-projects .gb-card:hover .gb-open { opacity: 1; transform: translateX(0); }
            .gb-projects .gb-icon {
            width: 46px; height: 46px; border-radius: 11px;
            display: flex; align-items: center; justify-content: center; font-size: 22px;
            }
            .gb-projects .gb-icon.free { background: rgba(var(--vz-primary-rgb), .12); color: var(--vz-primary); }
            .gb-projects .gb-icon.pro  { background: rgba(var(--vz-secondary-rgb,124,77,255), .14); color: var(--vz-secondary, #7c4dff); }
            .gb-projects .gb-plan {
            font-size: 11px; font-weight: 500; padding: 3px 10px; border-radius: 20px;
            }
            .gb-projects .gb-plan.free { background: var(--vz-light); color: var(--vz-secondary-color, #878a99); }
            .gb-projects .gb-plan.pro  { background: rgba(var(--vz-secondary-rgb,124,77,255), .14); color: var(--vz-secondary, #7c4dff); }
            .gb-projects .gb-foot {
            margin-top: auto; padding-top: 12px; border-top: 1px solid var(--vz-border-color);
            display: flex; align-items: center; justify-content: space-between;
            }
            .gb-projects .gb-open {
            color: var(--vz-primary); font-weight: 500; font-size: 13px;
            display: inline-flex; align-items: center; gap: 4px;
            opacity: .5; transform: translateX(-4px); transition: opacity .18s, transform .18s;
            }
            .gb-projects .gb-del {
            opacity: 0; transition: opacity .15s;
            }
            .gb-projects .gb-card:hover .gb-del { opacity: 1; }
            .gb-projects .gb-new {
            border: 1.5px dashed var(--vz-border-color); border-radius: var(--gb-r);
            display: flex; flex-direction: column; align-items: center; justify-content: center;
            gap: 8px; min-height: 180px; cursor: pointer;
            transition: border-color .18s, background .18s;
            }
            .gb-projects .gb-new:hover { border-color: var(--vz-primary); background: rgba(var(--vz-primary-rgb), .04); }
            .gb-projects .gb-new.disabled { cursor: not-allowed; opacity: .55; }
            .gb-projects .gb-new.disabled:hover { border-color: var(--vz-border-color); background: transparent; }
            .gb-projects .gb-usage {
            display: inline-flex; align-items: center; gap: 8px;
            background: var(--vz-light); border-radius: 8px; padding: 7px 14px;
            }
            .gb-projects .gb-bars { display: flex; gap: 3px; }
            .gb-projects .gb-bar { width: 8px; height: 18px; border-radius: 2px; background: var(--vz-border-color); }
            .gb-projects .gb-bar.on { background: var(--vz-primary); }
        `}</style>

        <div className="gb-projects">
            {/* ── BAŞLIK ── */}
            <div className="d-flex align-items-start justify-content-between mb-4 gap-3 flex-wrap">
            <div>
                <h4 className="mb-1 fw-semibold">Projelerim</h4>
                <p className="text-muted mb-0">
                Her proje kendi tabloları, dosyaları ve API anahtarlarıyla izole bir alan.
                </p>
            </div>
            {quota && (
                <div className="gb-usage">
                <div className="gb-bars">
                    {Array.from({ length: max }).map((_, i) => (
                    <span key={i} className={`gb-bar ${i < used ? "on" : ""}`}></span>
                    ))}
                </div>
                <span className="text-muted" style={{ fontSize: 13 }}>{used} / {max} proje</span>
                </div>
            )}
            </div>

            {isLoading ? (
            <div className="text-center text-muted py-5"><Spinner className="me-2" /> Projeler yükleniyor</div>
            ) : (
            <div className="gb-grid">
                {list.map((p) => {
                const isPro = p.plan === 1;
                return (
                    <div key={p.id} className="gb-card" onClick={() => openProject(p)}>
                    <div className="d-flex align-items-start justify-content-between mb-3">
                        <div className={`gb-icon ${isPro ? "pro" : "free"}`}>
                        <i className="ri-stack-line"></i>
                        </div>
                        <div className="d-flex align-items-center gap-2">
                        <span className={`gb-plan ${isPro ? "pro" : "free"}`}>{isPro ? "Pro" : "Free"}</span>
                        <Button id={`table-popconfirm-${p.id}`} 
                        color="ghost-danger" size="sm" className="gb-del" title="Sil"
                            disabled={deleteMut.isPending} onClick={(e)=> e.stopPropagation()}>
                            <i className="ri-delete-bin-line"></i>
                        </Button>  
                        <PopConfirm 
                            targetId={`table-popconfirm-${p.id}`}
                            type={ModalType.Alert}
                            message='Bu kaydı silmek istediğinizden emin misiniz?'
                            confirmText='Sil!'
                            onConfirm={(e) =>onDelete(e, p)} 
                            onClose={() => toast.error("Silme işlemi iptal edildi!")} 
                        />
                        </div>
                    </div>

                    <h6 className="fw-semibold mb-1 text-truncate" title={p.name}>{p.name}</h6>
                    <p className="text-muted small mb-3" style={{ minHeight: 36, lineHeight: 1.5 }}>
                        {p.description || "Açıklama yok"}
                    </p>

                    <div className="gb-foot">
                        <small className="text-muted">
                        <i className="ri-table-line me-1"></i>{p.tableCount} tablo
                        </small>
                        <span className="gb-open">Aç <i className="ri-arrow-right-line"></i></span>
                    </div>
                    </div>
                );
                })}

                {/* ── YENİ PROJE ── */}
                <div
                className={`gb-new ${!canCreate ? "disabled" : ""}`}
                onClick={() => canCreate && openModal()}
                title={canCreate ? "Yeni proje" : "Plan limitine ulaştınız"}
                >
                <div className="gb-icon free" style={{ background: "var(--vz-light)" }}>
                    <i className="ri-add-line"></i>
                </div>
                <span className="fw-medium">Yeni proje</span>
                <small className="text-muted">
                    {canCreate ? `${remaining} hakkın kaldı` : `Free planda ${max} proje limiti`}
                </small>
                </div>
            </div>
            )}
        </div>

        {/* ── MODAL ── */}
        <Modal isOpen={modalOpen} toggle={() => setModalOpen(false)} centered>
            <ModalHeader toggle={() => setModalOpen(false)}>Yeni proje</ModalHeader>
            <ModalBody>
            <div className="mb-3">
                <Label className="form-label">Proje adı <span className="text-danger">*</span></Label>
                <Input value={name} onChange={(e) => setName(e.target.value)}
                placeholder="Mobil Uygulamam"
                onKeyDown={(e) => { if (e.key === "Enter") submit(); }} autoFocus />
            </div>
            <div className="mb-1">
                <Label className="form-label">Açıklama <span className="text-muted">(opsiyonel)</span></Label>
                <Input type="textarea" rows={2} value={description}
                onChange={(e) => setDescription(e.target.value)} placeholder="Bu proje ne için?" />
            </div>
            {err && <div className="text-danger small mt-2"><i className="ri-error-warning-line me-1"></i>{err}</div>}
            </ModalBody>
            <ModalFooter>
            <Button color="light" onClick={() => setModalOpen(false)}>Vazgeç</Button>
            <Button color="primary" onClick={submit} disabled={createMut.isPending}>
                {createMut.isPending ? <><Spinner size="sm" className="me-1" /> Oluşturuluyor</> : "Oluştur"}
            </Button>
            </ModalFooter>
        </Modal>
        </div>
    );
};