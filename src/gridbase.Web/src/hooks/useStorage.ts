import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { viewFile } from "helpers/backend_helper";
import { FileListParams, getApiClient } from "helpers/fileApi_helper"; 
import { toast } from "react-toastify";

const client = getApiClient();

const STORAGE_KEY = "storage";

// ── Listele ──
export const useFileList = (params: FileListParams) =>
  useQuery({
    queryKey: [STORAGE_KEY, "files", params],
    queryFn: () => client.list(params), 
  });

// ── Yukle (File -> localName string) ──
export const useUploadFile = () => {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (file: File) => client.upload(file),
    onSuccess: () => qc.invalidateQueries({ queryKey: [STORAGE_KEY, "files"] }),
    onError: (err: any) => console.error("Upload failed:", err),
  });
}; 
export const useDownLoadFile = (fileName: string) =>
  useQuery({
    queryKey: [STORAGE_KEY, "download", fileName],
    queryFn: async () => await viewFile(fileName),
    enabled: !!fileName,
  });

// ── Sil (localName) ──
export const useDeleteFile = () => {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (fileName: string) => client.delete(fileName),
    onSuccess: () => {
      qc.invalidateQueries({ queryKey: [STORAGE_KEY, "files"] });
      toast.success("Silme işlemi başarılı!")
    },
    onError: (err: any) => console.error("Delete failed:", err),
  });
}; 