namespace grocery_mate_backend.Models.Authentication;

public class AuthenticationResponseDto
{
    public string Token { get; set; }

    public string Email { get; set; }
    public DateTime Expiration { get; set; }
    
    public Guid UserId { get; set; }
}