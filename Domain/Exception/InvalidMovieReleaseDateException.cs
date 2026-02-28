namespace Domain.Exception;

public class InvalidMovieReleaseDateException: DomainException
{
    public InvalidMovieReleaseDateException(DateTime date) 
        : base($"La fecha de estreno {date.ToShortDateString()} no es válida. Las películas deben ser posteriores al año 1900.")
    {
    }
}