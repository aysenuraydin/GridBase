using gridbase.Domain.Repositories;

namespace gridbase.WebApi.Middleware;

public class DynamicCorsMiddleware
{
    private readonly RequestDelegate _next;
    public const string ProjectHeader = "X-Project-Id";

    public DynamicCorsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IProjectCorsRepository corsRepo, IConfiguration config)
    {
        if (!context.Request.Path.StartsWithSegments("/api/gridbase"))
        {
            await _next(context);
            return;
        }

        var origin = context.Request.Headers["Origin"].ToString();
        if (string.IsNullOrEmpty(origin))
        {
            await _next(context);
            return;
        }

        var allowed = false;

        var panelOrigins = config.GetSection("Cors:PanelOrigins").Get<string[]>() ?? Array.Empty<string>();
        if (panelOrigins.Contains(origin))
        {
            allowed = true;
        }
        else
        {
            long? projectId = null;
            if (context.Request.Headers.TryGetValue(ProjectHeader, out var raw)
                && long.TryParse(raw.ToString(), out var pid))
                projectId = pid;

            if (projectId.HasValue)
                allowed = await corsRepo.IsOriginAllowedAsync(projectId.Value, origin, context.RequestAborted);
        }

        if (allowed)
        {
            var headers = context.Response.Headers;
            headers["Access-Control-Allow-Origin"] = origin;
            headers["Vary"] = "Origin";
            headers["Access-Control-Allow-Methods"] = "GET, POST, PUT, PATCH, DELETE, OPTIONS";
            headers["Access-Control-Allow-Headers"] = "Content-Type, Authorization, X-Project-Id, X-GridBase-Key";
            headers["Access-Control-Allow-Credentials"] = "true";
            headers["Access-Control-Max-Age"] = "3600";
        }

        if (HttpMethods.IsOptions(context.Request.Method))
        {
            context.Response.StatusCode = allowed ? StatusCodes.Status204NoContent : StatusCodes.Status403Forbidden;
            return;
        }

        await _next(context);
    }
}

public static class DynamicCorsMiddlewareExtensions
{
    public static IApplicationBuilder UseDynamicCors(this IApplicationBuilder app) =>
        app.UseMiddleware<DynamicCorsMiddleware>();
}