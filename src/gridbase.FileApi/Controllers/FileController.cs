using System.Text;
using Microsoft.AspNetCore.Mvc;
using gridbase.FileApi.Modules;
using Microsoft.AspNetCore.Authorization;
namespace gridbase.FileApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FileController(IFileService fileService) : ControllerBase
{
    private readonly IFileService _fileService = fileService;

    [HttpGet]
    public async Task<IActionResult> ListFiles(
        [FromQuery] string? search = null,
        [FromQuery] string? type = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 24)
    {
        var result = await _fileService.ListFilesAsync(search, type, page, pageSize);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> UploadFiles(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Dosya boş veya mevcut değil.");

        var fileName = await _fileService.UploadFileAsync(file);
        return Ok(fileName);
    }

    [HttpGet("{fileName}")]
    [AllowAnonymous]
    public async Task<IActionResult> DownloadFiles(string fileName)
    {
        FileDto? file = await _fileService.DownloadFileAsync(fileName);
        if (file == null)
            return NotFound("Dosya mevcut değil.");

        return File(file.Data, file.ContentType, file.Name);
    }

    [HttpDelete("{fileName}")]
    public async Task<IActionResult> DeleteFiles(string fileName)
    {
        var protectedFiles = new List<string>
        {
            "favicon.ico",
            "logo-dark.png",
            "logo-light.png",
            "logo-sm.png"
        };

        if (protectedFiles.Contains(fileName))
        {
            return Ok(new { message = "Sistem varlığı korundu, fiziksel silme atlandı." });
        }

        bool result = await _fileService.DeleteFileAsync(fileName);
        if (result)
            return Ok("Dosya başarıyla silindi.");

        return StatusCode(500, "Dosya silinirken bir hata oluştu.");
    }
}


