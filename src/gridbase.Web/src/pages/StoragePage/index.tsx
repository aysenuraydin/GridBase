import React, { useMemo, useRef, useState } from "react";
import {
  Card, CardBody, CardHeader, Row, Col, Input, Button, Badge, Spinner,
  Dropdown, DropdownToggle, DropdownMenu, DropdownItem,
} from "reactstrap";
import config from "config";
import { useDeleteFile, useFileList, useUploadFile } from "hooks/useStorage"; 
import { PopConfirm } from "components/Common/PopConfirm";
import { ModalType } from "common/enums/ModalType";
import { toast, ToastContainer } from "react-toastify";
import { Image } from 'antd';
import useThemeMode from "hooks/useThemeMode";

interface FileItem {
  id: number;
  originalName: string;
  localName: string;        // guid.ext — erisim anahtari
  contentType?: string | null;
  extension?: string | null;
  size: number;
  createdAt: string;
}

const fileUrl = (localName: string) =>
  `${config.api.FILE_API_URL}/File/${localName}`;

const isImage = (ct?: string | null) => !!ct && ct.startsWith("image/");

const humanSize = (bytes: number): string => {
  if (!bytes) return "0 B";
  const u = ["B", "KB", "MB", "GB"];
  const i = Math.floor(Math.log(bytes) / Math.log(1024));
  return `${(bytes / Math.pow(1024, i)).toFixed(i ? 1 : 0)} ${u[i]}`;
};

const fmtDate = (iso: string): string => {
  try {
    return new Date(iso).toLocaleDateString("tr-TR", { day: "2-digit", month: "short", year: "numeric" });
  } catch { return iso; }
};

// Uzantiya gore ikon (gorsel disi dosyalar icin)
const extIcon = (ext?: string | null): string => {
  const e = (ext ?? "").toLowerCase();
  if (["pdf"].includes(e)) return "ri-file-pdf-2-line text-danger";
  if (["doc", "docx"].includes(e)) return "ri-file-word-2-line text-primary";
  if (["xls", "xlsx", "csv"].includes(e)) return "ri-file-excel-2-line text-success";
  if (["ppt", "pptx"].includes(e)) return "ri-file-ppt-2-line text-warning";
  if (["zip", "rar", "7z"].includes(e)) return "ri-file-zip-line text-muted";
  if (["mp4", "mov", "avi", "mkv"].includes(e)) return "ri-film-line text-info";
  if (["mp3", "wav", "ogg"].includes(e)) return "ri-music-2-line text-info";
  if (["json", "xml", "txt", "md"].includes(e)) return "ri-file-text-line text-secondary";
  return "ri-file-3-line text-muted";
};

type ViewMode = "grid" | "list";
type TypeFilter = "" | "image" | "document";

const StoragePage: React.FC = () => {
  const { isDark } = useThemeMode();
  const [search, setSearch] = useState("");
  const [type, setType] = useState<TypeFilter>("");
  const [page, setPage] = useState(1);
  const [view, setView] = useState<ViewMode>("grid");
  const [copied, setCopied] = useState<string | null>(null);
  const fileInputRef = useRef<HTMLInputElement | null>(null);
  const [dragOver, setDragOver] = useState(false);

  const { data, isLoading, isFetching } = useFileList({ search, type, page, pageSize: 24 });
  const upload = useUploadFile();
  const del = useDeleteFile();

  const result = useMemo(() => {
    const d: any = data;
    if (!d) return { items: [] as FileItem[], total: 0, totalPages: 1, page: 1 };
    const r = d.data ?? d;
    return {
      items: (r.items ?? []) as FileItem[],
      total: r.total ?? 0,
      totalPages: r.totalPages ?? 1,
      page: r.page ?? 1,
    };
  }, [data]);

  const onPickFiles = () => fileInputRef.current?.click();

  const copyUrl = async (localName: string) => {
    try {
      await navigator.clipboard.writeText(fileUrl(localName));
      setCopied(localName);
      setTimeout(() => setCopied((c) => (c === localName ? null : c)), 1500);
    } catch { /* clipboard yoksa sessiz gec */ }
  };

  const onDelete = (item: FileItem) => {
    del.mutate(item.localName); 
  };
  const uploadFiles = async (files: File[]) => {
    for (const f of files) { 
      await upload.mutateAsync(f);
    }
  };

  const onFilesSelected = async (e: React.ChangeEvent<HTMLInputElement>) => {
    const files = Array.from(e.target.files ?? []);
    await uploadFiles(files);
    if (fileInputRef.current) fileInputRef.current.value = "";
  };

  const onDragOver = (e: React.DragEvent) => {
    e.preventDefault();       
    if (!dragOver) setDragOver(true);
  };
  const onDragLeave = (e: React.DragEvent) => {
    e.preventDefault();
    setDragOver(false);
  };
  const onDrop = async (e: React.DragEvent) => {
    e.preventDefault();
    setDragOver(false);
    const files = Array.from(e.dataTransfer.files ?? []);
    if (files.length) await uploadFiles(files);
  };

  const dummy = "https://dummyimage.com/100x100/" + (isDark ? "031426" : "F3F6F9") + "/" + (isDark ? "fff" : "969696") + "&text=Gridbase";

  const Thumb: React.FC<{ item: FileItem; size: number }> = ({ item, size }) => (
    isImage(item.contentType) ? (
      <Image
        src={fileUrl(item.localName)}
        alt={item.originalName}
        width={size}
        height={size} 
        fallback={dummy}
        className="rounded"
        loading="lazy"
        style={{ objectFit: 'cover' }}
        preview={true}
      />
    ) : (
      <div
        className="rounded d-flex align-items-center justify-content-center bg-light"
        style={{ width: size, height: size }}
      >
        <i className={extIcon(item.extension)} style={{ fontSize: size * 0.4 }}></i>
      </div>
    )
  );

  const Actions: React.FC<{ item: FileItem; compact?: boolean }> = ({ item, compact }) => (
    <div className={`d-flex gap-1 ${compact ? "" : "justify-content-center"}`}>
      <Button color="light" size="sm" className="border" title="URL kopyala" onClick={() => copyUrl(item.localName)}>
        <i className={copied === item.localName ? "ri-check-line text-success" : "ri-links-line"}></i>
      </Button>
      <a className="btn btn-light btn-sm border" title="Indir" href={fileUrl(item.localName)} target="_blank" rel="noreferrer" download>
        <i className="ri-download-2-line"></i>
      </a>
      <Button id={`file-popconfirm-${item?.id}`} color="ghost-danger" className="btn-soft-danger" size="sm" title="Sil" disabled={del.isPending}>
        <i className="ri-delete-bin-line"></i>
      </Button> 
      <PopConfirm 
          targetId={`file-popconfirm-${item?.id}`}
          type={ModalType.Alert}
          message='Bu kaydı silmek istediğinizden emin misiniz?'
          confirmText='Sil!'
          onConfirm={() => onDelete(item)} 
          onClose={() => toast.error("Silme işlemi iptal edildi!")} 
      />
    </div>
  );

  return (
    <div className="page-content">
      <style>{`
        .gb-storage .gb-file-card {
          border: 1px solid var(--vz-border-color); border-radius: 6px; overflow: hidden;
          transition: box-shadow .15s ease, transform .15s ease; height: 100%;
        }
        .gb-storage .gb-file-card:hover { box-shadow: 0 6px 18px rgba(0,0,0,.08); transform: translateY(-2px); }
        .gb-storage .gb-thumb-wrap {
          aspect-ratio: 1/1; display: flex; align-items: center; justify-content: center;
          background: var(--vz-light); overflow: hidden;
        }
        .gb-storage .gb-thumb-wrap img { width: 100%; height: 100%; object-fit: cover; }
        .gb-storage .gb-drop {
          border: 2px dashed var(--vz-border-color); border-radius: 12px;
          padding: 2.5rem 1rem; text-align: center; cursor: pointer; transition: border-color .15s, background .15s;
        }
        .gb-storage .gb-drop:hover { border-color: var(--vz-primary); background: rgba(var(--vz-primary-rgb), .04); }
        .gb-storage .gb-list-row { transition: background .12s ease; }
        .gb-storage .gb-list-row:hover { background: var(--vz-light); }
      `}</style>

      <div className="gb-storage">
        <Card className="shadow-none border">
          {/* ── UST BAR ── */}
          <CardHeader className="bg-transparent border-bottom py-3">
            <Row className="g-2 align-items-center">
              <Col xs="auto" className="d-flex align-items-center">
                <div className="avatar-xs me-2">
                  <span className="avatar-title bg-primary-subtle text-primary rounded fs-15">
                    <i className="ri-folder-3-line"></i>
                  </span>
                </div>
                <div>
                  <h6 className="mb-0 fw-semibold">Storage</h6>
                  <small className="text-muted">{result.total} dosya</small>
                </div>
              </Col>

              <Col>
                <div className="position-relative" style={{ maxWidth: 280 }}>
                  <i className="ri-search-line position-absolute text-muted" style={{ left: 10, top: 8, fontSize: 14 }}></i>
                  <Input
                    bsSize="sm"
                    value={search}
                    onChange={(e) => { setSearch(e.target.value); setPage(1); }}
                    placeholder="Dosya ara"
                    style={{ paddingLeft: 30 }}
                  />
                </div>
              </Col>

              <Col xs="auto">
                <div className="btn-group btn-group-sm" role="group">
                  <Button color={type === "" ? "primary" : "light"} onClick={() => { setType(""); setPage(1); }}>Hepsi</Button>
                  <Button color={type === "image" ? "primary" : "light"} onClick={() => { setType("image"); setPage(1); }}>Gorseller</Button>
                  <Button color={type === "document" ? "primary" : "light"} onClick={() => { setType("document"); setPage(1); }}>Belgeler</Button>
                </div>
              </Col>

              <Col xs="auto">
                <div className="btn-group btn-group-sm" role="group" title="Gorunum">
                  <Button color={view === "grid" ? "primary" : "light"} onClick={() => setView("grid")}>
                    <i className="ri-grid-fill"></i>
                  </Button>
                  <Button color={view === "list" ? "primary" : "light"} onClick={() => setView("list")}>
                    <i className="ri-list-unordered"></i>
                  </Button>
                </div>
              </Col>

              <Col xs="auto">
                <Button color="primary" size="sm" onClick={onPickFiles} disabled={upload.isPending}>
                  {upload.isPending
                    ? <><Spinner size="sm" className="me-1" /> Yukleniyor</>
                    : <><i className="ri-upload-2-line me-1"></i> Yukle</>}
                </Button>
                <input ref={fileInputRef} type="file" multiple hidden onChange={onFilesSelected} />
              </Col>
            </Row>
          </CardHeader>

          <CardBody>
            <div
              className={`gb-drop mb-3 ${dragOver ? "gb-drop-active" : ""}`}
              onClick={onPickFiles}
              onDragOver={onDragOver}
              onDragLeave={onDragLeave}
              onDrop={onDrop}
            >
              <i className="ri-upload-cloud-2-line display-5 text-muted opacity-50 d-block mb-2"></i>
              <h6 className="text-muted mb-1">
                {dragOver ? "Bırak, yükleyelim" : "Dosyaları buraya sürükle"}
              </h6>
              <p className="small text-muted mb-0">ya da tıkla / sağ üstteki Yükle'yi kullan.</p>
            </div>
            {isLoading ? (
              <div className="text-center text-muted py-5">
                <Spinner className="me-2" /> Dosyalar yukleniyor
              </div>
            ) : view === "grid" ? (
              // ─────────── GRID GORUNUM ───────────
              <Row className="g-3">
                {result.items.map((item) => (
                  <Col key={item.id} xs={6} sm={4} md={3} xl={2}>
                    <div className="gb-file-card">
                      <div className="gb-thumb-wrap">
                        {isImage(item.contentType) ? ( 
                          <Image
                            src={fileUrl(item.localName)}
                            alt={item.originalName} 
                            fallback={dummy}
                            className="rounded"
                            loading="lazy"
                            style={{ objectFit: 'cover', height: '100%', width: '100%' }} 
                            wrapperStyle={{ height: '100%', width: '100%' }} 
                            preview={true}
                          />
                        ) : (
                          <i className={extIcon(item.extension)} style={{ fontSize: 44 }}></i>
                        )}
                      </div>
                      <div className="p-2">
                        <div className="text-truncate small fw-medium" title={item.originalName}>
                          {item.originalName}.{item.extension}
                        </div>
                        <div className="d-flex justify-content-between align-items-center mt-1">
                          <small className="text-muted">{humanSize(item.size)}</small>
                          <Badge color="light" className="text-body fw-normal">{item.extension}</Badge>
                        </div>
                        <div className="mt-2">
                          <Actions item={item} />
                        </div>
                      </div>
                    </div>
                  </Col>
                ))}
              </Row>
            ) : (
              // ─────────── LISTE GORUNUM ───────────
              <div className="table-responsive">
                <table className="table align-middle table-borderless mb-0">
                  <thead className="text-muted" style={{ fontSize: 11, textTransform: "uppercase", letterSpacing: ".04em" }}>
                    <tr className="border-bottom">
                      <th style={{ width: 56 }}></th>
                      <th>Ad</th>
                      <th>Tip</th>
                      <th>Boyut</th>
                      <th>Tarih</th>
                      <th className="text-end">Islemler</th>
                    </tr>
                  </thead>
                  <tbody>
                    {result.items.map((item) => (
                      <tr key={item.id} className="gb-list-row border-bottom">
                        <td><Thumb item={item} size={40} /></td>
                        <td>
                          <div className="text-truncate fw-medium" style={{ maxWidth: 260 }} title={item.originalName}>
                            {item.originalName}.{item.extension}
                          </div>
                          <small className="text-muted">{item.contentType}</small>
                        </td>
                        <td><Badge color="light" className="text-body fw-normal">{item.extension}</Badge></td>
                        <td className="text-muted">{humanSize(item.size)}</td>
                        <td className="text-muted">{fmtDate(item.createdAt)}</td>
                        <td>
                          <div className="d-flex justify-content-end">
                            <Actions item={item} compact />
                          </div>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            )}

            {/* ── SAYFALAMA ── */}
            {result.totalPages > 1 && (
              <div className="d-flex align-items-center justify-content-between mt-3">
                <small className="text-muted">
                  {isFetching && <Spinner size="sm" className="me-1" />}
                  Sayfa {result.page} / {result.totalPages}
                </small>
                <div className="btn-group btn-group-sm">
                  <Button color="light" disabled={page <= 1} onClick={() => setPage((p) => Math.max(1, p - 1))}>
                    <i className="ri-arrow-left-s-line"></i> Onceki
                  </Button>
                  <Button color="light" disabled={page >= result.totalPages} onClick={() => setPage((p) => p + 1)}>
                    Sonraki <i className="ri-arrow-right-s-line"></i>
                  </Button>
                </div>
              </div>
            )}
          </CardBody>
        </Card>
      </div>
      <ToastContainer  closeButton={true}  limit={3} style={{marginTop:"100px"}}/>
    </div>
  );
};

export default StoragePage;