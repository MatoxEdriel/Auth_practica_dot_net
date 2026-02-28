namespace Domain.Exception;

public class InvalidTitleMovie: DomainException
{
    public InvalidTitleMovie() : 
        base($"no puede estar vacio ")
    {
    }
}