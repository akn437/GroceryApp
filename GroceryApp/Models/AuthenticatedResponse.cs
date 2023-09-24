namespace JwtAuthentication.Server.Models
{
    public class AuthenticatedResponse
    {
        public string? Token { get; set; }
        public string? UserName { get;  set; }
    }
}
