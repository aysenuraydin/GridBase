import React, { useMemo, useState } from "react";
import {
    Card, CardBody, CardHeader, Row, Col, Button, Badge, Input, Label, Spinner,
    Modal, ModalHeader, ModalBody, ModalFooter, Nav, NavItem, NavLink, TabContent, TabPane,
} from "reactstrap";
import { useProjectKeys, useCreateProjectKey, useRevokeProjectKey } from "hooks/useApiKeys";
import { ApiKeyListItem, CreatedApiKey } from "helpers/backend_helper";
import config from "config";
import { useProjectContext } from "context/ProjectContext";

const GRIDBASE_URL = config?.api?.API_URL;

const ApiKeysPage: React.FC = () => {
    const { selectedProjectId } = useProjectContext();
    const projectId = selectedProjectId ?? 0;

    const { data: keys, isLoading } = useProjectKeys(projectId || undefined);
    const createMut = useCreateProjectKey(projectId);
    const revokeMut = useRevokeProjectKey(projectId);

    const [tab, setTab] = useState<"keys" | "connect">("keys");
    const [modalOpen, setModalOpen] = useState(false);
    const [keyType, setKeyType] = useState<0 | 1>(0);
    const [keyName, setKeyName] = useState("");
    const [created, setCreated] = useState<CreatedApiKey | null>(null);
    const [copied, setCopied] = useState<string | null>(null);

    const list: ApiKeyListItem[] = Array.isArray(keys) ? keys : (keys as any)?.data ?? [];
    const anonKey = list.find((k) => k.keyType === 0 && k.isActive);

    // ── gerçek metrikler (listeden hesaplanır) ──
    const stats = useMemo(() => {
        const total = list.length;
        const activeAnon = list.filter((k) => k.keyType === 0 && k.isActive).length;
        const activeSecret = list.filter((k) => k.keyType === 1 && k.isActive).length;
        const revoked = list.filter((k) => !k.isActive).length;
        return { total, activeAnon, activeSecret, revoked };
    }, [list]);

    const copy = async (text: string, tag: string) => {
        try {
        await navigator.clipboard.writeText(text);
        setCopied(tag);
        setTimeout(() => setCopied((c) => (c === tag ? null : c)), 1500);
        } catch { /* yoksay */ }
    };

    const openCreate = () => { setKeyType(0); setKeyName(""); setCreated(null); setModalOpen(true); };

    const submitCreate = async () => {
        const res = await createMut.mutateAsync({ keyType, name: keyName.trim() || undefined });
        setCreated(res);
    };

    const onRevoke = (k: ApiKeyListItem) => {
        if (!window.confirm(`"${k.keyPrefix}" anahtarı iptal edilsin mi? Bu anahtarı kullanan uygulamalar erişimi kaybeder.`)) return;
        revokeMut.mutate(k.id);
    };

    if (!selectedProjectId) {
        return (
        <div className="page-content">
            <div className="text-center text-muted py-5">
            <i className="ri-key-2-line display-5 opacity-25 d-block mb-2"></i>
            Önce bir proje seç (üst bardan).
            </div>
        </div>
        );
    }

    const exampleKey = anonKey ? anonKey.keyPrefix : "gb_pk_live_xxxxxxxx";
    const fetchSnippet =
    `const GRIDBASE_URL = "${GRIDBASE_URL}";
    const API_KEY = "${exampleKey}";   // tam anahtarı Anahtarlar sekmesinden al

    const res = await fetch(\`\${GRIDBASE_URL}/api/gridbase/products\`, {
    headers: { "X-GridBase-Key": API_KEY }
    });
    const data = await res.json();`;

    const curlSnippet =
    `curl "${GRIDBASE_URL}/api/gridbase/products" \\
    -H "X-GridBase-Key: ${exampleKey}"`;

    const createSnippet =
    `await fetch(\`\${GRIDBASE_URL}/api/gridbase/products\`, {
    method: "POST",
    headers: {
        "X-GridBase-Key": API_KEY,
        "Content-Type": "application/json"
    },
    body: JSON.stringify({ name: "Yeni ürün", price: 99 })
    });`;

    return (
        <div className="page-content">
        <style>{`
            .gb-keys .gb-stat {
            background: var(--vz-card-bg); border: 1px solid var(--vz-border-color);
            border-radius: 12px; padding: 1.1rem 1.25rem; height: 100%;
            }
            .gb-keys .gb-stat-icon {
            width: 42px; height: 42px; border-radius: 10px;
            display: flex; align-items: center; justify-content: center; font-size: 20px;
            }
            .gb-keys .gb-code {
            background: var(--vz-light); border: 1px solid var(--vz-border-color);
            border-radius: 8px; padding: 1rem; font-family: var(--bs-font-monospace);
            font-size: 12.5px; white-space: pre; overflow-x: auto; position: relative;
            }
            .gb-keys .gb-copy-btn { position: absolute; top: 8px; right: 8px; }
            .gb-keys .gb-key-mono { font-family: var(--bs-font-monospace); }
            .gb-keys .gb-row:hover { background: var(--vz-light); }
            .gb-keys .gb-row { transition: background .12s ease; }
        `}</style>

        <div className="gb-keys">
            {/* ── BAŞLIK ── */}
            <div className="d-flex align-items-start justify-content-between mb-3 gap-3 flex-wrap">
            <div>
                <h4 className="mb-1 fw-semibold">API Anahtarları</h4>
                <p className="text-muted mb-0">Bu projeye dışarıdan bağlanmak için anahtarlar ve kod örnekleri.</p>
            </div>
            <Button color="primary" onClick={openCreate}>
                <i className="ri-add-line me-1"></i> Yeni anahtar
            </Button>
            </div>

            {/* ── WIDGET'LAR (gerçek veriden) ── */}
            <Row className="g-3 mb-1">
            <Col md={4}>
                <div className="gb-stat">
                <div className="d-flex justify-content-between align-items-start">
                    <div>
                    <p className="text-muted mb-1" style={{ fontSize: 13 }}>Toplam anahtar</p>
                    <h3 className="mb-0 fw-semibold">{stats.total}</h3>
                    </div>
                    <div className="gb-stat-icon bg-primary-subtle text-primary">
                    <i className="ri-key-2-line"></i>
                    </div>
                </div>
                <p className="text-muted mb-0 mt-2" style={{ fontSize: 12 }}>
                    {stats.revoked > 0 ? `${stats.revoked} iptal edilmiş` : "hepsi aktif"}
                </p>
                </div>
            </Col>

            <Col md={4}>
                <div className="gb-stat">
                <div className="d-flex justify-content-between align-items-start">
                    <div>
                    <p className="text-muted mb-1" style={{ fontSize: 13 }}>Anon (aktif)</p>
                    <h3 className="mb-0 fw-semibold">{stats.activeAnon}</h3>
                    </div>
                    <div className="gb-stat-icon bg-success-subtle text-success">
                    <i className="ri-global-line"></i>
                    </div>
                </div>
                <p className="text-muted mb-0 mt-2" style={{ fontSize: 12 }}>frontend, kurallara tabi</p>
                </div>
            </Col>

            <Col md={4}>
                <div className="gb-stat">
                <div className="d-flex justify-content-between align-items-start">
                    <div>
                    <p className="text-muted mb-1" style={{ fontSize: 13 }}>Secret (aktif)</p>
                    <h3 className="mb-0 fw-semibold">{stats.activeSecret}</h3>
                    </div>
                    <div className="gb-stat-icon bg-danger-subtle text-danger">
                    <i className="ri-lock-2-line"></i>
                    </div>
                </div>
                <p className="text-muted mb-0 mt-2" style={{ fontSize: 12 }}>backend, kuralları bypass</p>
                </div>
            </Col>
            </Row>

            {/* ── TABLAR + İÇERİK ── */}
            <Card className="shadow-none border mt-3">
            <CardHeader className="bg-transparent border-bottom p-0">
                <Nav className="nav-tabs nav-tabs-custom nav-primary border-0">
                <NavItem>
                    <NavLink className={tab === "keys" ? "active" : ""} onClick={() => setTab("keys")} style={{ cursor: "pointer" }}>
                    <i className="ri-key-2-line me-1"></i> Anahtarlar
                    </NavLink>
                </NavItem>
                <NavItem>
                    <NavLink className={tab === "connect" ? "active" : ""} onClick={() => setTab("connect")} style={{ cursor: "pointer" }}>
                    <i className="ri-plug-line me-1"></i> Bağlan
                    </NavLink>
                </NavItem>
                </Nav>
            </CardHeader>

            <CardBody>
                <TabContent activeTab={tab}>
                {/* ── ANAHTARLAR ── */}
                <TabPane tabId="keys">
                    <div className="text-muted small mb-3">
                    <i className="ri-information-line me-1"></i>
                    <strong>anon</strong> anahtarı frontend'de güvenle kullanılır (tablo kurallarına tabi).
                    <strong className="ms-1">secret</strong> anahtarı yalnız backend'de — kuralları bypass eder.
                    </div>

                    {isLoading ? (
                    <div className="text-center text-muted py-4"><Spinner size="sm" className="me-2" /> Yükleniyor</div>
                    ) : list.length === 0 ? (
                    <div className="text-center text-muted py-5">
                        <i className="ri-key-2-line display-6 opacity-25 d-block mb-2"></i>
                        <p className="mb-2">Henüz anahtar yok.</p>
                        <Button color="primary" size="sm" onClick={openCreate}>
                        <i className="ri-add-line me-1"></i> İlk anahtarını oluştur
                        </Button>
                    </div>
                    ) : (
                    <div className="table-responsive">
                        <table className="table align-middle table-borderless mb-0">
                        <thead className="text-muted" style={{ fontSize: 11, textTransform: "uppercase", letterSpacing: ".04em" }}>
                            <tr className="border-bottom">
                            <th>Tip</th><th>Anahtar</th><th>Ad</th><th>Son kullanım</th><th>Durum</th><th></th>
                            </tr>
                        </thead>
                        <tbody>
                            {list.map((k) => (
                            <tr key={k.id} className="gb-row border-bottom">
                                <td>
                                <Badge color={k.keyType === 1 ? "danger-subtle" : "success-subtle"}
                                    className={k.keyType === 1 ? "text-danger" : "text-success"}>
                                    {k.keyType === 1 ? "secret" : "anon"}
                                </Badge>
                                </td>
                                <td className="gb-key-mono text-muted">{k.keyPrefix}</td>
                                <td>{k.name || <span className="text-muted">—</span>}</td>
                                <td className="text-muted">{k.lastUsedAt ? new Date(k.lastUsedAt).toLocaleDateString("tr-TR") : "—"}</td>
                                <td>
                                {k.isActive
                                    ? <Badge color="success-subtle" className="text-success">aktif</Badge>
                                    : <Badge color="secondary-subtle" className="text-muted">iptal</Badge>}
                                </td>
                                <td className="text-end">
                                {k.isActive && (
                                    <Button color="ghost-danger" size="sm" onClick={() => onRevoke(k)} disabled={revokeMut.isPending}>
                                    <i className="ri-forbid-line me-1"></i> İptal
                                    </Button>
                                )}
                                </td>
                            </tr>
                            ))}
                        </tbody>
                        </table>
                    </div>
                    )}
                </TabPane>

                {/* ── BAĞLAN ── */}
                <TabPane tabId="connect">
                    <p className="text-muted">
                    Uygulamandan GridBase'e böyle bağlanırsın. Tam anahtarı "Anahtarlar"
                    sekmesinden al (anahtar oluştururken bir kez gösterilir).
                    </p>

                    <Label className="form-label fw-semibold mt-2">JavaScript (fetch)</Label>
                    <div className="gb-code">
                    <Button color="light" size="sm" className="gb-copy-btn border" onClick={() => copy(fetchSnippet, "fetch")}>
                        <i className={copied === "fetch" ? "ri-check-line text-success" : "ri-file-copy-line"}></i>
                    </Button>
                    {fetchSnippet}
                    </div>

                    <Label className="form-label fw-semibold mt-3">Kayıt oluştur (POST)</Label>
                    <div className="gb-code">
                    <Button color="light" size="sm" className="gb-copy-btn border" onClick={() => copy(createSnippet, "create")}>
                        <i className={copied === "create" ? "ri-check-line text-success" : "ri-file-copy-line"}></i>
                    </Button>
                    {createSnippet}
                    </div>

                    <Label className="form-label fw-semibold mt-3">cURL</Label>
                    <div className="gb-code">
                    <Button color="light" size="sm" className="gb-copy-btn border" onClick={() => copy(curlSnippet, "curl")}>
                        <i className={copied === "curl" ? "ri-check-line text-success" : "ri-file-copy-line"}></i>
                    </Button>
                    {curlSnippet}
                    </div>
                </TabPane>
                </TabContent>
            </CardBody>
            </Card>
        </div>

        {/* ── YENİ ANAHTAR MODAL ── */}
        <Modal isOpen={modalOpen} toggle={() => setModalOpen(false)} centered>
            <ModalHeader toggle={() => setModalOpen(false)}>
            {created ? "Anahtar oluşturuldu" : "Yeni API anahtarı"}
            </ModalHeader>
            <ModalBody>
            {created ? (
                <>
                <div className="alert alert-warning d-flex align-items-start gap-2">
                    <i className="ri-error-warning-line fs-18"></i>
                    <div className="small">
                    Bu anahtar <strong>sadece şimdi</strong> gösteriliyor. Kopyala ve güvenli sakla —
                    tekrar göremezsin.
                    </div>
                </div>
                <Label className="form-label fw-semibold">Anahtarın ({created.keyType === 1 ? "secret" : "anon"})</Label>
                <div className="d-flex gap-2">
                    <Input readOnly value={created.rawKey} className="gb-key-mono" style={{ fontSize: 12.5 }} />
                    <Button color="primary" onClick={() => copy(created.rawKey, "raw")}>
                    <i className={copied === "raw" ? "ri-check-line" : "ri-file-copy-line"}></i>
                    </Button>
                </div>
                </>
            ) : (
                <>
                <div className="mb-3">
                    <Label className="form-label">Anahtar tipi</Label>
                    <div className="d-flex gap-2">
                    <Button color={keyType === 0 ? "primary" : "light"} className="flex-grow-1" onClick={() => setKeyType(0)}>
                        <div className="fw-semibold">anon</div>
                        <small className={keyType === 0 ? "text-white-50" : "text-muted"}>frontend, güvenli</small>
                    </Button>
                    <Button color={keyType === 1 ? "danger" : "light"} className="flex-grow-1" onClick={() => setKeyType(1)}>
                        <div className="fw-semibold">secret</div>
                        <small className={keyType === 1 ? "text-white-50" : "text-muted"}>backend, bypass</small>
                    </Button>
                    </div>
                </div>
                <div className="mb-1">
                    <Label className="form-label">Ad <span className="text-muted">(opsiyonel)</span></Label>
                    <Input value={keyName} onChange={(e) => setKeyName(e.target.value)} placeholder="örn: Mobil uygulama" />
                </div>
                </>
            )}
            </ModalBody>
            <ModalFooter>
            {created ? (
                <Button color="primary" onClick={() => setModalOpen(false)}>Tamam, kaydettim</Button>
            ) : (
                <>
                <Button color="light" onClick={() => setModalOpen(false)}>Vazgeç</Button>
                <Button color="primary" onClick={submitCreate} disabled={createMut.isPending}>
                    {createMut.isPending ? <><Spinner size="sm" className="me-1" /> Oluşturuluyor</> : "Oluştur"}
                </Button>
                </>
            )}
            </ModalFooter>
        </Modal>
        </div>
    );
};

export default ApiKeysPage;