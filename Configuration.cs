namespace NewBlog;
public static class Configuration
{
    public static string JwtKey = "2E31E972-A815-4960-B76B-ECAECFF323CC";
    public static string ApiKeyName = "api_key";
    public static string ApiKey = "curso_api";
    public static SmtpConfigurantion Smtp = new();

    public class SmtpConfigurantion
    {
        public string Host { get; set; }
        public int Port { get; set; } = 25;
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
