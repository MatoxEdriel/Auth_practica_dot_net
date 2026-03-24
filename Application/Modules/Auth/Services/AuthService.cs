using Application.Modules.Auth.DTOs.Requests;
using Application.Modules.Auth.DTOs.Response;
using Application.Modules.Auth.Interface;
using Domain.Entities.Auth.Interfaces;
using Domain.Enums;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Crypto.Generators;

namespace Application.Modules.Auth.Services;

public class AuthService: IAuthService
{
    
    private readonly IAuthRepository _repository;
    private readonly ISecurityService _securityService;
    private readonly IJwtProvider _jwtProvider;
    

    public AuthService(
        IAuthRepository repository, 
        IJwtProvider jwtProvider,
        ISecurityService securityService
        )
    {
        _repository = repository;
        _jwtProvider = jwtProvider;
        _securityService = securityService;
    }
    
    

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
    {

        var user = await _repository.GetUserByLoginAsync(request.Identifier);

        if (user == null)
            throw new UnauthorizedAccessException("Credenciales inválidas.");
        
        bool isPasswordValid = _securityService.VerifyPassword(request.Password, user.PasswordHash);

        
        if (!isPasswordValid)
            throw new UnauthorizedAccessException("Credenciales inválidas.");
        if (user.IsTwoFactorEnabled)
        {
            
            string otpCode = _securityService.GenerateSecureOtp();
            DateTime expiration = DateTime.UtcNow.AddMinutes(5);
            await _repository.SaveOtpAsync(user.Id, OtpActionType.Login, otpCode, expiration);
            
            return new LoginResponseDto
            {
                RequiresTwoFactor = true,
                Message = "Se ha enviado un código de verificación."
            }; 
        }
        string token = _jwtProvider.GenerateAccessToken(user);
        
        return new LoginResponseDto
        {
            RequiresTwoFactor = false,
            AccessToken = token,
            Message = "Login exitoso."
        };
    }

    public async Task<LoginResponseDto> VerifyOtpLoginAsync(VerifyOtpRequestDto request)
    {
      
        var user = await _repository.GetUserByLoginAsync(request.Identifier);
        
        if (user == null)
            throw new UnauthorizedAccessException("El codigo es invalido o ha expirado.");
        
        bool isOtpValid = await _repository.VerifyOtpAsync(user.Id, OtpActionType.Login, request.OtpCode);
        
        if (!isOtpValid)
            throw new UnauthorizedAccessException("El codigo es invalido o ha expirado.");
        
        string token = _jwtProvider.GenerateAccessToken(user);        
        
        return new LoginResponseDto
        {
            RequiresTwoFactor = false, 
            AccessToken = token,      
            Message = "Verificación exitosa. Sesión iniciada."
        };
        
        
        
        
    }
}