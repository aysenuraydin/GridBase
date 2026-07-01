import React, { useState } from "react";
import { Input, Button, Spinner } from "reactstrap";
import { useCorsOrigins, useAddCorsOrigin, useRemoveCorsOrigin } from "hooks/useCors";
import { CorsOriginItem } from "helpers/backend_helper";

const CorsSettings: React.FC<{ projectId: number }> = ({ projectId }) => {
    const { data, isLoading } = useCorsOrigins(projectId || undefined);
    const addMut = useAddCorsOrigin(projectId);
    const removeMut = useRemoveCorsOrigin(projectId);

    const [origin, setOrigin] = useState("");
    const [err, setErr] = useState("");

    const list: CorsOriginItem[] = Array.isArray(data) ? data : (data as any)?.data ?? [];

    const add = async () => {
        const val = origin.trim();
        if (!val) { setErr("Origin gir."); return; }
        setErr("");
        try {
        await addMut.mutateAsync(val);
        setOrigin("");
        } catch (e: any) {
        const msg = e?.response?.data?.message || e?.response?.data || e?.message || "Eklenemedi.";
        setErr(typeof msg === "string" ? msg : "Eklenemedi.");
        }
    };

    const remove = (o: CorsOriginItem) => {
        if (!window.confirm(`"${o.origin}" kaldırılsın mı? Bu origin'den gelen istekler artık reddedilir.`)) return;
        removeMut.mutate(o.id);
    };

    const isWildcard = (o: string) => o === "*";

    return (
        <div className="border rounded mb-3" style={{ background: "var(--vz-card-bg)" }}>
        <style>{`
            .gb-cors .gb-cors-row {
            display: flex; align-items: center; justify-content: space-between;
            padding: 10px 12px; background: var(--vz-light); border-radius: 8px;
            transition: background .12s ease;
            }
            .gb-cors .gb-cors-row:hover { background: var(--vz-border-color); }
            .gb-cors .gb-cors-mono { font-family: var(--bs-font-monospace); font-size: 13px; }
            .gb-cors .gb-cors-x {
            border: 0; background: transparent; color: var(--vz-secondary-color, #878a99);
            font-size: 16px; line-height: 1; cursor: pointer; padding: 2px 6px; border-radius: 5px;
            }
            .gb-cors .gb-cors-x:hover { background: rgba(var(--vz-danger-rgb), .12); color: var(--vz-danger); }
        `}</style>

        <div className="gb-cors p-3 p-md-4">
            {/* ── başlık ── */}
            <div className="d-flex align-items-start gap-3 mb-3">
            <div className="flex-shrink-0 d-flex align-items-center justify-content-center"
                style={{ width: 40, height: 40, borderRadius: 10, background: "rgba(var(--vz-primary-rgb), .12)" }}>
                <i className="ri-global-line text-primary" style={{ fontSize: 20 }}></i>
            </div>
            <div>
                <div className="fw-semibold">İzinli origin'ler</div>
                <div className="text-muted small" style={{ lineHeight: 1.5 }}>
                Bu projenin API'sine tarayıcıdan hangi adresler erişebilir. Örn:{" "}
                <code>https://uygulamam.com</code>. Tümü için <code>*</code> (üretimde önerilmez).
                </div>
            </div>
            </div>

            {/* ── ekleme ── */}
            <div className="d-flex gap-2 mb-3">
            <Input
                value={origin}
                onChange={(e) => setOrigin(e.target.value)}
                placeholder="https://uygulamam.com"
                onKeyDown={(e) => { if (e.key === "Enter") add(); }}
            />
            <Button color="primary" onClick={add} disabled={addMut.isPending} style={{ whiteSpace: "nowrap" }}>
                {addMut.isPending ? <Spinner size="sm" /> : <><i className="ri-add-line me-1"></i>Ekle</>}
            </Button>
            </div>
            {err && <div className="text-danger small mb-2"><i className="ri-error-warning-line me-1"></i>{err}</div>}

            {/* ── liste ── */}
            {isLoading ? (
            <div className="text-muted small"><Spinner size="sm" className="me-2" />Yükleniyor</div>
            ) : list.length === 0 ? (
            <div className="text-muted small text-center py-4">
                <i className="ri-global-line opacity-25 d-block mb-1" style={{ fontSize: 28 }}></i>
                Henüz izinli origin yok. Dışarıdan tarayıcı erişimi için ekle.
            </div>
            ) : (
            <div className="d-flex flex-column gap-2">
                {list.map((o) => (
                <div key={o.id} className="gb-cors-row">
                    <span className="d-flex align-items-center gap-2 text-truncate">
                    <i className={isWildcard(o.origin) ? "ri-alert-line text-warning" : "ri-checkbox-circle-line text-success"}
                        style={{ fontSize: 16, flexShrink: 0 }}></i>
                    <code className="gb-cors-mono text-truncate">{o.origin}</code>
                    {isWildcard(o.origin) && (
                        <span className="badge bg-warning-subtle text-warning fw-normal flex-shrink-0">tüm adresler</span>
                    )}
                    </span>
                    <button type="button" className="gb-cors-x flex-shrink-0"
                    title="Kaldır" onClick={() => remove(o)} disabled={removeMut.isPending}>
                    <i className="ri-close-line"></i>
                    </button>
                </div>
                ))}
            </div>
            )}
        </div>
        </div>
    );
};

export default CorsSettings;