import React, { useState, useEffect } from "react";
import {
    Button, Input, Label, Spinner, Row, Col,
    Modal, ModalHeader, ModalBody, ModalFooter,
} from "reactstrap";
import { useSetTableAccess, useGridbaseSchema, useGetTableAccess } from "hooks/useGridBase"; 

const ACCESS_LEVELS = [
    { value: "Public",        label: "Herkese açık",   desc: "Anon key ile herkes erişir" },
    { value: "Authenticated", label: "Giriş yapanlar", desc: "Token gerekli" },
    { value: "RoleBased",     label: "Role göre",      desc: "Belirli roller" },
    { value: "Owner",         label: "Sadece sahibi",  desc: "Kendi kayıtları" },
];

interface AccessForm {
    readAccess: string;
    writeAccess: string;
    readRequiredRole: string;
    writeRequiredRole: string;
    isOwnerScoped: boolean;
    ownerColumn: string;
}

const defaults: AccessForm = {
    readAccess: "Public",
    writeAccess: "Authenticated",
    readRequiredRole: "",
    writeRequiredRole: "",
    isOwnerScoped: false,
    ownerColumn: "",
};

export const AccessModal: React.FC<{
    isOpen: boolean;
    toggle: () => void;
    tableName: string;
    tableId?: number;
    onDone?: () => void;
}> = ({ isOpen, toggle, tableName, tableId, onDone }) => {
    const setAccessMut = useSetTableAccess(tableName);
    const { data: schemaResp } = useGridbaseSchema(tableName);

    // ── mevcut erişim ayarlarını çek (sadece modal açıkken + tableId varken) ──
    const { data: accessResp, isLoading: accessLoading } = useGetTableAccess<any>(
        isOpen && tableId ? tableId : 0
    );

    const [form, setForm] = useState<AccessForm>(defaults);
    const [err, setErr] = useState("");

  // tablonun kolonları (ownerColumn seçimi için)
    const columns: any[] = (() => {
        const d: any = schemaResp;
        const cols = d?.columns ?? d?.data?.columns ?? [];
        return Array.isArray(cols) ? cols.filter((c: any) => !c.isForeign) : [];
    })();

  // modal açılınca + access verisi gelince formu doldur
    useEffect(() => {
        if (!isOpen) return;
        setErr("");
        const a = accessResp?.data ?? accessResp ?? null;
        if (a) {
        setForm({
            readAccess: a.readAccess ?? "Public",
            writeAccess: a.writeAccess ?? "Authenticated",
            readRequiredRole: a.readRequiredRole ?? "",
            writeRequiredRole: a.writeRequiredRole ?? "",
            isOwnerScoped: a.isOwnerScoped ?? false,
            ownerColumn: a.ownerColumn ?? "",
        });
        } else {
        setForm(defaults);
        }
    }, [isOpen, accessResp]);

    const set = (patch: Partial<AccessForm>) => setForm((f) => ({ ...f, ...patch }));

    const submit = async () => {
        setErr("");
        if (form.readAccess === "RoleBased" && !form.readRequiredRole.trim()) {
        setErr("Okuma 'Role göre' seçili — okuma rolü gir."); return;
        }
        if (form.writeAccess === "RoleBased" && !form.writeRequiredRole.trim()) {
        setErr("Yazma 'Role göre' seçili — yazma rolü gir."); return;
        }
        if (form.isOwnerScoped && !form.ownerColumn) {
        setErr("Sahip-bazlı açık — sahip kolonunu seç."); return;
        }

    try {
        await setAccessMut.mutateAsync({
            readAccess: form.readAccess,
            writeAccess: form.writeAccess,
            readRequiredRole: form.readAccess === "RoleBased" ? form.readRequiredRole.trim() : undefined,
            writeRequiredRole: form.writeAccess === "RoleBased" ? form.writeRequiredRole.trim() : undefined,
            isOwnerScoped: form.isOwnerScoped,
            ownerColumn: form.isOwnerScoped ? form.ownerColumn : undefined,
        });
        onDone?.();
        toggle();
    } catch (e: any) {
        const msg = e?.response?.data?.message || e?.response?.data || e?.message || "Kaydedilemedi.";
        setErr(typeof msg === "string" ? msg : "Kaydedilemedi.");
    }
};

    const LevelPicker: React.FC<{
        value: string; onChange: (v: string) => void; idPrefix: string;
    }> = ({ value, onChange, idPrefix }) => (
        <div className="d-flex flex-column gap-2">
        {ACCESS_LEVELS.map((lvl) => (
            <label key={lvl.value}
            className={`d-flex align-items-start gap-2 p-2 rounded border ${value === lvl.value ? "border-primary bg-primary-subtle" : "border"}`}
            style={{ cursor: "pointer" }}>
            <input type="radio" className="form-check-input mt-1" name={idPrefix}
                checked={value === lvl.value} onChange={() => onChange(lvl.value)} />
            <span>
                <span className="fw-medium d-block" style={{ fontSize: 13 }}>{lvl.label}</span>
                <span className="text-muted" style={{ fontSize: 11 }}>{lvl.desc}</span>
            </span>
            </label>
        ))}
        </div>
    );

    const busy = setAccessMut.isPending;

    return (
        <Modal isOpen={isOpen} toggle={toggle} centered size="lg">
        <ModalHeader toggle={toggle}>
            <i className="ri-shield-keyhole-line me-2"></i>
            Erişim kuralları — <code className="text-primary">{tableName}</code>
        </ModalHeader>
        <ModalBody>
            {accessLoading ? (
            <div className="text-center text-muted py-4">
                <Spinner size="sm" className="me-2" /> Mevcut kurallar yükleniyor
            </div>
            ) : (
            <>
                <p className="text-muted small mb-3">
                Bu tabloya API key ile gelen isteklerin erişim kuralları. Anon key bu
                kurallara tabidir; secret key hepsini geçer.
                </p>

                <Row className="g-4">
                <Col md={6}>
                    <div className="d-flex align-items-center mb-2">
                    <i className="ri-eye-line me-2 text-primary"></i>
                    <span className="fw-semibold">Okuma erişimi</span>
                    </div>
                    <LevelPicker value={form.readAccess} onChange={(v) => set({ readAccess: v })} idPrefix="read" />
                    {form.readAccess === "RoleBased" && (
                    <div className="mt-2">
                        <Label className="form-label small">Okuma rolleri <span className="text-muted">(virgülle)</span></Label>
                        <Input bsSize="sm" value={form.readRequiredRole}
                        onChange={(e) => set({ readRequiredRole: e.target.value })} placeholder="Admin, Editor" />
                    </div>
                    )}
                </Col>

                <Col md={6}>
                    <div className="d-flex align-items-center mb-2">
                    <i className="ri-edit-line me-2 text-warning"></i>
                    <span className="fw-semibold">Yazma erişimi</span>
                    </div>
                    <LevelPicker value={form.writeAccess} onChange={(v) => set({ writeAccess: v })} idPrefix="write" />
                    {form.writeAccess === "RoleBased" && (
                    <div className="mt-2">
                        <Label className="form-label small">Yazma rolleri <span className="text-muted">(virgülle)</span></Label>
                        <Input bsSize="sm" value={form.writeRequiredRole}
                        onChange={(e) => set({ writeRequiredRole: e.target.value })} placeholder="Admin" />
                    </div>
                    )}
                </Col>
                </Row>

                <div className="mt-4 pt-3 border-top">
                <div className="form-check form-switch mb-2">
                    <Input type="switch" role="switch" id="ownerScoped"
                    checked={form.isOwnerScoped}
                    onChange={(e) => set({ isOwnerScoped: (e.target as HTMLInputElement).checked })} />
                    <Label className="form-check-label fw-medium" htmlFor="ownerScoped">Sahip-bazlı erişim</Label>
                </div>
                <p className="text-muted small mb-2">
                    Açıksa: her kullanıcı yalnızca kendi oluşturduğu kayıtları görür/düzenler.
                    Kayıt oluşturulurken sahip otomatik damgalanır.
                </p>
                {form.isOwnerScoped && (
                    <div>
                    <Label className="form-label small">Sahip kolonu</Label>
                    <Input type="select" bsSize="sm" value={form.ownerColumn}
                        onChange={(e) => set({ ownerColumn: e.target.value })}>
                        <option value="">— kolon seç —</option>
                        {columns.map((c) => <option key={c.key} value={c.key}>{c.label}</option>)}
                    </Input>
                    <div className="text-muted small mt-1">
                        Kullanıcı id'sinin yazılacağı kolon (örn: userId, ownerId).
                    </div>
                    </div>
                )}
                </div>

                {err && <div className="text-danger small mt-3"><i className="ri-error-warning-line me-1"></i>{err}</div>}
            </>
            )}
        </ModalBody>
        <ModalFooter>
            <Button color="light" onClick={toggle}>Vazgeç</Button>
            <Button color="primary" onClick={submit} disabled={busy || accessLoading}>
            {busy ? <><Spinner size="sm" className="me-1" /> Kaydediliyor</> : "Kuralları kaydet"}
            </Button>
        </ModalFooter>
        </Modal>
    );
};