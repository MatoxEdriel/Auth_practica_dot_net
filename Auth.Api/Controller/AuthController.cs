using Application.Modules.Auth.DTOs.Requests;
using Application.Modules.Auth.Interface;
using Microsoft.AspNetCore.Mvc;


namespace Auth.Api.Controller;



[ApiController]
[Route("api/[controller]")]
public class AuthController: ControllerBase
{
    private readonly IAuthService _authService;
    
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        try
        {
            var response = await _authService.LoginAsync(request);
            
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
           
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception)
        {
        
            return StatusCode(500, new { message = "Ocurrió un error interno en el servidor." });
        }
    }

   
    [HttpPost("verify-otp")]
    public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequestDto request)
    {
  
        try
        {
            var response = await _authService.VerifyOtpLoginAsync(request);
            
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "Ocurrió un error interno en el servidor." });
        }
    }
    
}