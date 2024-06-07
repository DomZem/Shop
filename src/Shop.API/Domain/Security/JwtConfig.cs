namespace Shop.API.Domain.Security
{
    public class JwtConfig
    {
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public string Secret { get; set; }
    }
}
