using gridbase.FileApi.Data;

namespace gridbase.FileApi.Seeders;

public static class FileSeeder
{
    private static readonly (string Local, string Original, string ContentType, string Ext)[] SystemFiles =
    {
        ("favicon.ico",    "favicon",    "image/x-icon", "ico"),
        ("logo-dark.png",  "logo-dark",  "image/png",    "png"),
        ("logo-light.png", "logo-light", "image/png",    "png"),
        ("logo-sm.png",    "logo-sm",    "image/png",    "png"),
    };

    public static async Task SeedAsync(FileDbContext db, IWebHostEnvironment env, IConfiguration config)
    {
        foreach (var f in SystemFiles)
        {
            var exists = db.Files.Any(x => x.LocalName == f.Local);
            if (exists) continue;

            var baseLocation = Path.Combine(
                env.ContentRootPath,
                config["FileSaveLocation"] ?? "UploadedFiles");

            var filePath = Path.Combine(baseLocation, f.Local);

            long size = 0;
            if (System.IO.File.Exists(filePath))
                size = new FileInfo(filePath).Length;

            var entity = new FileEntity
            {
                OriginalName = f.Original,
                LocalName = f.Local,
                ContentType = f.ContentType,
                Extension = f.Ext,
                Size = size,
                FilePath = filePath,
                CreatedAt = DateTime.UtcNow,
                ProjectId = 0,
                OwnerUserId = null
            };

            await db.Files.AddAsync(entity);
        }

        await db.SaveChangesAsync();
    }
}