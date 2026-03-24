namespace Domain.Entities.Auth.Interfaces;

public interface ISecurityService
{
    string GenerateSecureOtp();
    string GenerateRandomToken(int length = 32); 
    string HashPassword(string plainPassword);
    bool VerifyPassword(string plainPassword, string hashedPassword);
    bool IsPasswordStrongEnough(string password);
    
}