namespace Application.Modules.Movies.DTOs.Requests;

public class CreateMovieRequest
{
    public string Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int RoomId { get; set; }
    
    
    
}