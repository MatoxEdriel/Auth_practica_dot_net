using Application.Modules.Movies.DTOs.Requests;
using Application.Modules.Movies.DTOs.Responses;
using Application.Modules.Movies.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Modules.Movies.Services;

public class MovieService: IMovieService
{
    private readonly IMovieRepository _movieRepository;
    public async Task<int> CreateMovieAsync(CreateMovieRequest request)
    {

        if (string.IsNullOrWhiteSpace(request.Title))
        {
            throw new ArgumentException("El título de la película es obligatorio.");
        }
        
        var newMovie = new Movie(
            title: request.Title, 
            releaseDate: request.ReleaseDate, 
            roomId: request.RoomId
        );
        int movieId = await _movieRepository.CreateAsync(newMovie);
        return movieId;
    }

    public async Task<IEnumerable<MovieResponse>> SearchMoviesAsync(MovieFilter filter)
    {
        var moviesFromDatabase = await _movieRepository.SearchAsync(filter);
        var result = moviesFromDatabase.Select(movie => new MovieResponse
        {
            Id = movie.Id,
            Title = movie.Title,
            ReleaseDate = movie.ReleaseDate.ToString("yyyy-MM-dd"), 
            RoomId = movie.RoomId
        });
        return result;
    }
}