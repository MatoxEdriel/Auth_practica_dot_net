using Application.Modules.Movies.DTOs.Requests;
using Application.Modules.Movies.Filters;
using Application.Modules.Movies.Interfaces;
using Intercore.shared.DTOs;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using MovieFilter = Domain.Entities.MovieFilter;

namespace Auth.Api.Controller;




[ApiController]
[Route("api/movies")]
public class MovieController:ControllerBase
{
    private readonly IMovieService _movieService;
    //producer creado 
    private readonly ITopicProducer<CreateAppLogDto> _appLogProducer;
    
    public MovieController(IMovieService movieService, ITopicProducer<CreateAppLogDto> appLogProducer)
    {
        _appLogProducer = appLogProducer;
        _movieService = movieService;
    }
    
    
    
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateMovieRequest request)
    {
        int newId = await _movieService.CreateMovieAsync(request);
        
        //como es un app y varia  ala logica de negocio seria algo asi 
        await _appLogProducer.Produce(new CreateAppLogDto
        {
            UserId = "SISTEMA",
            Module = "MOVIES",
            Action = "MOVIE_CREATED",
            Payload = new Dictionary<string, object>
            {
                { "message", $"Se creó la película: {request.Title}" },
                { "title", request.Title },
                { "movieId", newId }
            }
            
        });

        return Ok(new { MovieId = newId });
    }
    [HttpGet("test-error")]
    public IActionResult TestError()
    {

        throw new Exception(" Error de prueba: xd.");
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