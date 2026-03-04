using Application.Modules.Tickets.Interfaces;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controller;

[Route("api/[controller]")]
public class TicketController: ControllerBase
{
    private readonly ITicketService _ticketService;
    
    public TicketController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }
    
    [HttpGet]
    public IActionResult GetTickets([FromQuery] TicketFilter filter)
    {
        try
        {
            var tickets = _ticketService.GetTickets(filter);

            return Ok(tickets);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocurrió un error: {ex.Message}");
        }
    }
    
    
    
    
    
}