namespace MircroShop.Services.AuthAPI.Models
{
    public class AuthConfig
    {
        public string JWT_KEY { get; set; } = string.Empty;
        public string ISSUER { get; set; } = string.Empty;
        public string AUDIENCE { get; set; } = string.Empty;
        public int DURATION_IN_MINS { get; set; }
    }
}
