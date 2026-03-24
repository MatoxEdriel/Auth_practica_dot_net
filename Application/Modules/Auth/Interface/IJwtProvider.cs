using Domain.Entities.Auth;

namespace Application.Modules.Auth.Interface;

public interface IJwtProvider
{
    string GenerateAccessToken(User user);
    string GenerateActionToken(User user, string actionType);
}