import axios, { AxiosInstance } from "axios";
import config from "config";
import { getSelectedProjectId } from "context/ProjectContext";  

export interface FileListItem {
  id: number;
  originalName: string;
  localName: string;
  contentType?: string | null;
  extension?: string | null;
  size: number;
  createdAt: string;
}

export interface FileListResult {
  page: number;
  pageSize: number;
  total: number;
  totalPages: number;
  items: FileListItem[];
}

export interface FileListParams {
  search?: string;
  type?: string;
  page?: number;
  pageSize?: number;
}

class FileAPIClient {
  private axiosInstance: AxiosInstance;

  constructor() {
    this.axiosInstance = axios.create({
      baseURL: config.api.FILE_API_URL,
    });

    this.axiosInstance.interceptors.request.use(
      (cfg) => {
        const authUser = localStorage.getItem("authUser");
        if (authUser) {
          const token = JSON.parse(authUser).token?.replace(/^"+|"+$/g, "").trim();
          if (token) cfg.headers["Authorization"] = `Bearer ${token}`;
        }

        // ── seçili proje (Faz 4 — storage proje-scope) ──
        const projectId = getSelectedProjectId();
        if (projectId != null) cfg.headers["X-Project-Id"] = String(projectId);

        return cfg;
      },
      (error) => Promise.reject(error)
    );

    this.axiosInstance.interceptors.response.use(
      (response) => response.data ?? response,
      (error) => Promise.reject(error)
    );
  }

  view = (fileName: string): Promise<Blob> =>
    this.axiosInstance.get(`/File/${fileName}`, {
      responseType: "blob"
    });

  upload = (file: File): Promise<string> => {
    const formData = new FormData();
    formData.append("file", file);
    return this.axiosInstance.post("/File", formData, {
      headers: {
        "Content-Type": "multipart/form-data"
      }
    });
  };

  list = (params: FileListParams = {}): Promise<FileListResult> =>
    this.axiosInstance.get("/File", { params });

  delete = (fileName: string): Promise<void> =>
    this.axiosInstance.delete(`/File/${fileName}`);
}

const getLoggedinUser = () => {
  const user = localStorage.getItem("authUser");
  return user ? JSON.parse(user) : null;
};

const setAuthorization = (token: string) => {};

let apiClientInstance: FileAPIClient | null = null;
const getApiClient = () => {
  if (!apiClientInstance) apiClientInstance = new FileAPIClient();
  return apiClientInstance;
};

export { FileAPIClient, getLoggedinUser, getApiClient, setAuthorization };