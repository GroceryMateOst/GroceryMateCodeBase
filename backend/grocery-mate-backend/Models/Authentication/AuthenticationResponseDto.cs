namespace grocery_mate_backend.Models;

public class AuthenticationResponseDto
{
    public string Token { get; set; }

    public string Email { get; set; }
    public DateTime Expiration { get; set; }
}