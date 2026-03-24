namespace Domain.Exception.Auth;

public class UserNotActiveException : System.Exception
{
    public UserNotActiveException(string email) 
        : base($"El usuario con el correo {email} está inactivo o bloqueado.")
    {
    }
}