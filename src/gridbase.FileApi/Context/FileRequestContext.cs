using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace gridbase.FileApi.Context;

public interface IFileRequestContext
{
    long? ProjectId { get; }
    string? UserId { get; }
    bool HasProject { get; }
}

public class FileRequestContext : IFileRequestContext
{
    public long? ProjectId { get; }
    public string? UserId { get; }
    public bool HasProject => ProjectId.HasValue;

    public const string ProjectHeader = "X-Project-Id";
    // API key handler'ın ürettiği claim (end-user)
    public const string ClaimProjectId = "gb_project_id";

    public FileRequestContext(IHttpContextAccessor accessor)
    {
        var http = accessor.HttpContext;
        if (http is null) return;

        // ── projectId çözümü — ÖNCELİK SIRASI ──
        // 1) API key claim (gb_project_id) → end-user (key ile gelir, header yok)
        // 2) X-Project-Id header           → developer paneli (JWT akışı)
        var claimVal = http.User?.FindFirst(ClaimProjectId)?.Value;
        if (long.TryParse(claimVal, out var claimPid))
        {
            ProjectId = claimPid;
        }
        else if (http.Request.Headers.TryGetValue(ProjectHeader, out var raw)
                    && long.TryParse(raw.ToString(), out var headerPid))
        {
            ProjectId = headerPid;
        }

        // ── userId: JWT claim (developer). End-user'da olmayabilir. ──
        UserId = http.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? http.User?.FindFirst("sub")?.Value
                ?? http.User?.FindFirst("nameid")?.Value;
    }
}