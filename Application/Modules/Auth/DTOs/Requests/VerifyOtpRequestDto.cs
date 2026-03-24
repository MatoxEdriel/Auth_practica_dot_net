namespace Application.Modules.Auth.DTOs.Requests;

public class VerifyOtpRequestDto
{
    public string Identifier { get; set; } = string.Empty;
    public string OtpCode { get; set; } = string.Empty;
}