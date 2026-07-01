namespace gridbase.FileApi.Modules;

public interface IFileService
{
    Task<FileListResult> ListFilesAsync(
    string? search = null, string? type = null, int page = 1, int pageSize = 24);
    Task<string> UploadFileAsync(IFormFile file);
    Task<FileDto?> DownloadFileAsync(string fileName);
    Task<bool> DeleteFileAsync(string fileName);
}