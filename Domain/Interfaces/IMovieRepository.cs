using Domain.Entities;

namespace Domain.Interfaces;

public interface IMovieRepository
{
    void Create(Movie movie);
    
    //practica buena practica concepto 
    Task<int> CreateAsync(Movie movie);
    
    
    
    Movie GetById(int id);
    void Update(Movie movie);
    void Delete(int id);
    IEnumerable<Movie> GetByName(string name);
    IEnumerable<Movie> GetByReleaseDate(DateTime releaseDate);
    string GetRoomStatus(string roomName);
    
}