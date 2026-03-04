using Domain.Entities;

namespace Domain.Interfaces;


public interface ITicketRepository
{
    IEnumerable<Ticket> GetAll(TicketFilter filter);
}