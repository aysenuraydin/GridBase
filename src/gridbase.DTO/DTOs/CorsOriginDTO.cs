namespace gridbase.DTO.DTOs
{
    public sealed class AddCorsOriginRequest
    {
        public string Origin { get; set; } = null!;   // "https://app.com"
    }

    public sealed class CorsOriginItem
    {
        public long Id { get; set; }
        public string Origin { get; set; } = null!;
        public System.DateTime CreatedAt { get; set; }
    }
}
