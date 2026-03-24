using Domain.Entities.Auth;

namespace Application.Modules.Auth.Interface;

public interface IJwtProvider
{
    string Generate(User user);
}