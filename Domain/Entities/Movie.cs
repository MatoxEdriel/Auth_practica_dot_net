using Domain.Exception;

//Estandar de entidad clase 
namespace Domain.Entities;
//crear clase ricas en su instancias no se permita instanciar sin ciertos datos
public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int RoomId { get; set; }
    
    public Room Room { get; set; }
    
    
    protected Movie() { }

    public Movie(string title, DateTime releaseDate, int roomId)
    {
        if (releaseDate.Year < 1900)
        {
            throw new InvalidMovieReleaseDateException(releaseDate);
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new InvalidTitleMovie();
        }

        Title = title;
        ReleaseDate = releaseDate;
        RoomId = roomId;
    }
    
    
    
    
}



