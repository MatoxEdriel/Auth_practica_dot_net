using System.Security.Cryptography;
using Domain.Entities.Auth.Interfaces;
namespace Infrastructure.Modules.Auth;
using BCrypt.Net; 
public class SecurityService: ISecurityService
{
    public string GenerateSecureOtp()
    {
        int randomOtp = RandomNumberGenerator.GetInt32(100000, 1000000);
        return randomOtp.ToString();
    }
    public string HashPassword(string plainPassword)
    {
        return BCrypt.HashPassword(plainPassword, workFactor: 12);
        
        
    }

    public bool VerifyPassword(string plainPassword, string hashedPassword)
    {
        return BCrypt.Verify(plainPassword, hashedPassword);
        
        
    }

    public bool IsPasswordStrongEnough(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            return false; 

        bool hasUpper = password.Any(char.IsUpper);  
        bool hasLower = password.Any(char.IsLower); 
        bool hasDigit = password.Any(char.IsDigit);   
        
        bool hasSpecial = password.Any(ch => !char.IsLetterOrDigit(ch));
        
        return hasUpper && hasLower && hasDigit && hasSpecial;
    }
}