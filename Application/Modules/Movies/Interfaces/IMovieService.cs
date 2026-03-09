using Application.Modules.Movies.DTOs.Requests;
using Application.Modules.Movies.DTOs.Responses;
using Domain.Entities;

namespace Application.Modules.Movies.Interfaces;

public interface IMovieService
{
    Task<int> CreateMovieAsync(CreateMovieRequest request);

    Task<IEnumerable<MovieResponse>> SearchMoviesAsync(MovieFilter filter);

}