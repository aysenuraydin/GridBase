using FluentScheduler;
using Serilog;
using gridbase.Infrastructure.Persistence;
using gridbase.WebApi.Constants;
using gridbase.WebApi.Endpoints;
using gridbase.WebApi.Jobs;
using gridbase.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddOpenApi();

builder.WebHost.UseUrls(
    builder.Configuration[ApplicationSettings.API_URLS]!
// "http://localhost:5222"
);

builder.Host.UseSerilog();

builder.Services
        .AddApplicationServices()
        .AddInfrastructureServices(builder.Configuration)
        .AddWebApiServices(builder.Configuration);

var app = builder.Build();

JobManager.Initialize(new MyRegistry(app.Services));
app.UseExceptionHandler(_ => { });

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();
app.UseDynamicCors();
app.UseCors(ApplicationSettings.CORS_KEY);

app.UseAuthentication();
app.UseAuthorization();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GridBase API V1");
        c.RoutePrefix = string.Empty;
    });

    try
    {
        await app.InitializeDb();
    }
    catch (Exception ex)
    {
        Console.WriteLine("DB initialization failed: " + ex);
    }
}

app.UseProjectContext();

app.MapControllers();
app.MapAuthEndpoints();
app.MapFallbackToFile("/app/index.html");

app.Run();



