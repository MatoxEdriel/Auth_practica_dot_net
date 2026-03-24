namespace Application.Modules.Auth.DTOs.Response;

public class LoginResponseDto
{
    public bool RequiresTwoFactor { get; set; }
    public string? AccessToken { get; set; }
    public string? Message { get; set; }
}

