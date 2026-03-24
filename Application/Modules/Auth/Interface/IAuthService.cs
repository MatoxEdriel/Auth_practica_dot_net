using Application.Modules.Auth.DTOs.Requests;
using Application.Modules.Auth.DTOs.Response;

namespace Application.Modules.Auth.Interface;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
    Task<LoginResponseDto> VerifyOtpLoginAsync(VerifyOtpRequestDto request);
}