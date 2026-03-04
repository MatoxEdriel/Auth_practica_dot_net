using Application.Modules.Tickets.DTOs;
using Application.Modules.Tickets.Interfaces;
using Domain.Interfaces;

namespace Application.Modules.Tickets.Services;

public class TicketService: ITicketService
{
    private readonly ITicketRepository _ticketRepository;
    
    public TicketService(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }
    
    public IEnumerable<TicketResponseDto> GetTickets(TicketFilter filter)
    {
        // 1. Pedimos los datos al Dominio (Aquí es donde internamente actuará Dapper)
        // Dapper nos devolverá la Entidad Ticket con el Customer, Movie y Room anidados.
        var ticketsFromDomain = _ticketRepository.GetAll(filter);

        // 2. Transformamos (Mapeamos) las Entidades ricas en DTOs planos para el usuario
        var result = ticketsFromDomain.Select(ticket => new TicketResponseDto
        {
            TicketId = ticket.Id,
            Price = ticket.Price,
            PurchaseDate = ticket.PurchaseDate,
            
            // Navegamos por los objetos anidados que Dapper armó por nosotros:
            CustomerName = ticket.Customer.FullName,
            MovieTitle = ticket.Movie.Title,
            RoomName = ticket.Movie.Room.Name
        });

        // 3. Devolvemos la lista limpia y lista para convertirse en JSON
        return result;
        
    }
}