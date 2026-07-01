import { ProjectOverview } from "common/data/ProjectOverview";
import { useProjectContext } from "context/ProjectContext";
import { useProjectOverview } from "hooks/useProject";
import React from "react";
import { useNavigate } from "react-router-dom";
import { Card, CardBody, CardHeader, Row, Col, Badge, Spinner, Button, Progress } from "reactstrap";

const humanSize = (bytes: number): string => {
    if (!bytes) return "0 B";
    const u = ["B", "KB", "MB", "GB"];
    const i = Math.floor(Math.log(bytes) / Math.log(1024));
    return `${(bytes / Math.pow(1024, i)).toFixed(i ? 1 : 0)} ${u[i]}`;
};

const StatCard: React.FC<{
    icon: string; color: string; label: string; value: React.ReactNode; sub?: string;
}> = ({ icon, color, label, value, sub }) => (
    <Card className="shadow-none border h-100">
        <CardBody>
            <div className="d-flex align-items-center">
                <div className="flex-shrink-0">
                    <div className="rounded d-flex align-items-center justify-content-center"
                        style={{ width: 48, height: 48, background: `rgba(var(--vz-${color}-rgb), .12)` }}>
                        <i className={`${icon} fs-22`} style={{ color: `var(--vz-${color})` }}></i>
                    </div>
                </div>
                <div className="flex-grow-1 ms-3">
                    <p className="text-muted mb-1 fs-13">{label}</p>
                    <h4 className="mb-0">{value}</h4>
                    {sub && <small className="text-muted">{sub}</small>}
                </div>
            </div>
        </CardBody>
    </Card>
);

const DashboardPage: React.FC = () => {
    const navigate = useNavigate();
    const { selectedProjectId } = useProjectContext();
    const projectId = selectedProjectId ?? 0;

    const { data, isLoading } = useProjectOverview(projectId || undefined);
    const ov: ProjectOverview | null = (data as any)?.data ?? (data as any) ?? null;

    if (!selectedProjectId) {
        return (
            <div className="page-content">
                <div className="text-center text-muted py-5">
                    <i className="ri-dashboard-3-line display-5 opacity-25 d-block mb-2"></i>
                    Önce bir proje seç (üst bardan).
                </div>
            </div>
        );
    }

    if (isLoading || !ov) {
        return (
            <div className="page-content">
                <div className="text-center text-muted py-5"><Spinner className="me-2" /> Yükleniyor</div>
            </div>
        );
    }

    const tablePct = ov.maxTables > 0 ? Math.min(100, Math.round((ov.tableCount / ov.maxTables) * 100)) : 0;
    const storageMb = ov.storageBytes / (1024 * 1024);
    const storagePct = ov.maxStorageMb > 0 ? Math.min(100, Math.round((storageMb / ov.maxStorageMb) * 100)) : 0;

    return (
        <div className="page-content">
            {/* ── BAŞLIK ── */}
            <Row className="mb-3 align-items-center">
                <Col>
                    <div className="d-flex align-items-center">
                        <div className="rounded d-flex align-items-center justify-content-center me-3"
                            style={{ width: 44, height: 44, background: "rgba(var(--vz-primary-rgb), .12)" }}>
                            <i className="ri-stack-line fs-22 text-primary"></i>
                        </div>
                        <div>
                            <h4 className="mb-0 fw-semibold">{ov.projectName}</h4>
                            <small className="text-muted">
                                Genel bakış · {new Date(ov.createdAt).toLocaleDateString("tr-TR")} tarihinde oluşturuldu
                            </small>
                        </div>
                    </div>
                </Col>
                <Col xs="auto">
                    <Badge color={ov.plan === "Pro" ? "primary-subtle" : "secondary-subtle"} className={ov.plan === "Pro" ? "text-primary" : "text-muted"}>
                        {ov.plan} plan
                    </Badge>
                </Col>
            </Row>

            {/* ── İSTATİSTİKLER ── */}
            <Row className="g-3 mb-1">
                <Col sm={6} xl={3}>
                    <StatCard icon="ri-table-line" color="primary" label="Tablo" value={ov.tableCount} sub={`${ov.maxTables} limit`} />
                </Col>
                <Col sm={6} xl={3}>
                    <StatCard icon="ri-list-check-2" color="info" label="Toplam satır" value={ov.totalRows.toLocaleString("tr-TR")} />
                </Col>
                <Col sm={6} xl={3}>
                    <StatCard icon="ri-folder-3-line" color="warning" label="Dosya" value={ov.fileCount > 0 ? ov.fileCount : "—"}
                        sub={ov.storageBytes > 0 ? humanSize(ov.storageBytes) : undefined} />
                </Col>
                <Col sm={6} xl={3}>
                    <StatCard icon="ri-key-2-line" color="success" label="Aktif anahtar" value={ov.activeKeyCount} />
                </Col>
            </Row>

            <Row className="g-3 mt-1">
                <Col lg={7}>
                    <Card className="shadow-none border h-100">
                        <CardHeader className="bg-transparent border-bottom d-flex align-items-center justify-content-between">
                            <span className="fw-semibold">Son tablolar</span>
                            <Button color="ghost-primary" size="sm" onClick={() => navigate("/datatables")}>
                                Tümü <i className="ri-arrow-right-s-line"></i>
                            </Button>
                        </CardHeader>
                        <CardBody className="p-0">
                            {ov.recentTables.length === 0 ? (
                                <div className="text-center text-muted py-4">
                                    <i className="ri-table-line display-6 opacity-25 d-block mb-2"></i>
                                    Henüz tablo yok.
                                    <div className="mt-2">
                                        <Button color="soft-primary" size="sm" onClick={() => navigate("/datatables")}>
                                            <i className="ri-add-line me-1"></i> Tablo oluştur
                                        </Button>
                                    </div>
                                </div>
                            ) : (
                                <div className="table-responsive">
                                    <table className="table align-middle table-borderless mb-0">
                                        <tbody>
                                            {ov.recentTables.map((t) => (
                                                <tr key={t.id} className="border-bottom"
                                                    style={{ cursor: "pointer" }} onClick={() => navigate(`/datatable/${t.id}`)}>
                                                    <td style={{ width: 44 }}>
                                                        <div className="rounded d-flex align-items-center justify-content-center"
                                                            style={{ width: 36, height: 36, background: "var(--vz-light)" }}>
                                                            <i className="ri-table-line text-muted"></i>
                                                        </div>
                                                    </td>
                                                    <td><span className="fw-medium">{t.name}</span></td>
                                                    <td className="text-muted">{t.rowCount} satır</td>
                                                    <td className="text-muted">{t.columnCount} kolon</td>
                                                    <td className="text-end">
                                                        <Button color="ghost-secondary" size="sm" title="API Console'da aç"
                                                            onClick={(e) => { e.stopPropagation(); navigate(`/console?table=${t.name}`); }}>
                                                            <i className="ri-terminal-box-line"></i>
                                                        </Button>
                                                    </td>
                                                </tr>
                                            ))}
                                        </tbody>
                                    </table>
                                </div>
                            )}
                        </CardBody>
                    </Card>
                </Col>

                {/* ── KULLANIM + KISAYOLLAR ── */}
                <Col lg={5}>
                    {/* kullanım */}
                    <Card className="shadow-none border mb-3">
                        <CardHeader className="bg-transparent border-bottom"><span className="fw-semibold">Kullanım</span></CardHeader>
                        <CardBody>
                            <div className="mb-3">
                                <div className="d-flex justify-content-between mb-1">
                                    <small className="text-muted">Tablolar</small>
                                    <small className="text-muted">{ov.tableCount} / {ov.maxTables}</small>
                                </div>
                                <Progress value={tablePct} color={tablePct > 85 ? "danger" : "primary"} style={{ height: 6 }} />
                            </div>
                            <div>
                                <div className="d-flex justify-content-between mb-1">
                                    <small className="text-muted">Depolama</small>
                                    <small className="text-muted">{storageMb.toFixed(1)} / {ov.maxStorageMb} MB</small>
                                </div>
                                <Progress value={storagePct} color={storagePct > 85 ? "danger" : "success"} style={{ height: 6 }} />
                            </div>
                        </CardBody>
                    </Card>

                    {/* kısayollar */}
                    <Card className="shadow-none border">
                        <CardHeader className="bg-transparent border-bottom"><span className="fw-semibold">Kısayollar</span></CardHeader>
                        <CardBody className="d-flex flex-column gap-2">
                            <Button color="soft-primary" className="text-start" onClick={() => navigate("/console")}>
                                <i className="ri-terminal-box-line me-2"></i> API Console
                            </Button>
                            <Button color="soft-primary" className="text-start" onClick={() => navigate("/storage")}>
                                <i className="ri-folder-3-line me-2"></i> Storage
                            </Button>
                            <Button color="soft-primary" className="text-start" onClick={() => navigate("/keys")}>
                                <i className="ri-key-2-line me-2"></i> API Anahtarları
                            </Button>
                            <Button color="soft-primary" className="text-start" onClick={() => navigate("/project-settings")}>
                                <i className="ri-settings-3-line me-2"></i> Proje Ayarları
                            </Button>
                        </CardBody>
                    </Card>
                </Col>
            </Row>
        </div>
    );
};

export default DashboardPage;