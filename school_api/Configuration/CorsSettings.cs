namespace school_api.Configuration
{
    public class CorsSettings
    {
        public const string SectionName = "Cors";
        public string[] AllowedOrigins { get; set; } = Array.Empty<string>();
    }
}
