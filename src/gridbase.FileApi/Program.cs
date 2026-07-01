using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using gridbase.FileApi.Data;
using gridbase.FileApi.Constants;
using gridbase.FileApi.Modules;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using gridbase.FileApi.Context;
using gridbase.FileApi.Seeders;
using gridbase.FileApi.Auth;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IFileRequestContext, FileRequestContext>();

builder.Services.AddDbContext<FileDbContext>(opt =>
{
    opt.UseSqlite(
        builder.Configuration.GetConnectionString(ConnectionSettings.DB_CONNECTION),
        b => b.MigrationsAssembly("gridbase.FileApi")
    );
});

builder.Services.AddScoped<IFileService, FileService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Smart";
    options.DefaultChallengeScheme = "Smart";
})
.AddPolicyScheme("Smart", "JWT veya API key", options =>
{
    options.ForwardDefaultSelector = context =>
    {
        if (context.Request.Headers.ContainsKey(GridBaseKeyAuthHandler.HeaderName))
            return GridBaseKeyAuthHandler.SchemeName;
        return JwtBearerDefaults.AuthenticationScheme;
    };
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Authentication:Jwt:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Authentication:Jwt:SigningKey"]!))
    };
})
.AddScheme<GridBaseKeyAuthOptions, GridBaseKeyAuthHandler>(
    GridBaseKeyAuthHandler.SchemeName, _ => { });

builder.Services.AddAuthorization();

builder.Services.AddHttpClient("GridBase", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["GridBase:BaseUrl"]!);
    c.Timeout = TimeSpan.FromSeconds(10);
});
builder.Services.AddMemoryCache();

builder.Services.AddControllers();

var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>() ?? Array.Empty<string>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(ApplicationSettings.CORS_KEY, policy =>
    {
        policy.WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GridBase File API",
        Version = "v1"
    });

    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Bearer {token}",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<FileDbContext>();
    var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
    var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    await FileSeeder.SeedAsync(db, env, config);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GridBase File API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(ApplicationSettings.CORS_KEY);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();