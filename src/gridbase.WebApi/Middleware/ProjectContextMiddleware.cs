using gridbase.Application.Common.Interfaces;
using gridbase.Domain.Repositories;

namespace gridbase.WebApi.Middleware;

public class ProjectContextMiddleware
{
    private readonly RequestDelegate _next;
    public const string HeaderName = "X-Project-Id";
    public const string ApiKeyHeader = "X-GridBase-Key";

    public ProjectContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext httpContext,
        IProjectContext projectContext,
        IUser currentUser,
        IProjectRepository projectRepository)
    {
        // 🔒 Hidden. Akış:
        //   1) API key claim'leri varsa → projectId + tip (Secret/Anon) çöz →
        //      ProjectContext'i kur (secret → servis seviyesi bypass) → devam.
        //   2) Aksi halde JWT: X-Project-Id header → projeyi getir →
        //      sahip ya da Admin/GB ise ProjectContext'i kur → devam.
        throw new NotImplementedException("Source available on request.");
    }
}

public static class ProjectContextMiddlewareExtensions
{
    public static IApplicationBuilder UseProjectContext(this IApplicationBuilder app) =>
        app.UseMiddleware<ProjectContextMiddleware>();
}