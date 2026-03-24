using Domain.Enums;

namespace Domain.Entities.Auth.Interfaces;

public interface IAuthRepository
{
    Task<User?> GetUserByLoginAsync(string identifier);
    Task SaveOtpAsync(Guid userId, OtpActionType actionType, string otpCode, DateTime expirationDate);
    Task<bool> VerifyOtpAsync(Guid userId, OtpActionType actionType, string otpCode);
    
}