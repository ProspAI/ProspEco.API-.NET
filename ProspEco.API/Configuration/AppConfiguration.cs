// ProspEco.API/Configuration/AppConfiguration.cs
namespace ProspEco.API.Configuration
{
    public class AppConfiguration
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public SwaggerSettings Swagger { get; set; }
    }

    public class ConnectionStrings
    {
        public string? DefaultConnection { get; set; }
        public string? OracleFIAP { get; set; }
    }

    public class SwaggerSettings
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}