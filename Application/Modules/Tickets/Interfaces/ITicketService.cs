using Application.Modules.Tickets.DTOs;
using Domain.Interfaces;

namespace Application.Modules.Tickets.Interfaces;

public interface ITicketService
{
    // Recibe los filtros del Dominio, pero devuelve DTOs de la Aplicación
    IEnumerable<TicketResponseDto> GetTickets(TicketFilter filter);
}