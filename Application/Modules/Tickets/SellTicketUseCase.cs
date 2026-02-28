using Domain.Entities;
using Domain.Interfaces.Room;

namespace Application.Modules.Tickets;

public class SellTicketUseCase
{
    private readonly ISeatAvailabilityChecker _seatChecker;
    
    public SellTicketUseCase(ISeatAvailabilityChecker seatChecker)
    {
        _seatChecker = seatChecker;
    }
    
    public void Execute(string seatNumberFromWeb)
    {

        var ticket = new Ticket(seatNumberFromWeb, _seatChecker);

    }
    
    
    
    
}