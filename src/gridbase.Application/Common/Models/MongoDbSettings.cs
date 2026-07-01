
namespace gridbase.Application.Common.Models;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string CellsCollection { get; set; } = null!;
}