using Application.Modules.Movies.DTOs.Requests;
using Application.Modules.Movies.Filters;
using Application.Modules.Movies.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MovieFilter = Domain.Entities.MovieFilter;

namespace Auth.Api.Controller;




[ApiController]
[Route("api/movies")]
public class MovieController:ControllerBase
{
    private readonly IMovieService _movieService;
    
    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateMovieRequest request)
    {
        int newId = await _movieService.CreateMovieAsync(request);

        return Ok(new { MovieId = newId });
    }
    
    [HttpPost("search")]
    public async Task<IActionResult> Search([FromBody] MovieFilter filter)
    {
        var movies = await _movieService.SearchMoviesAsync(filter);

        return Ok(movies);
    }
    
    /*
    public async Task<IActionResult> Create([FromBody] CreateMovieRequest request)
    {
        try
        {
            int newId = await _movieService.CreateMovieAsync(request);
            return Ok(new 
            { 
                Success = true, 
                Message = "Película guardada exitosamente en la base de datos.", 
                MovieId = newId 
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new 
            { 
                Success = false, 
                Error = ex.Message 
            });
        }
    }
    */
    
    
    
  
    
    
    
    
    
    
}