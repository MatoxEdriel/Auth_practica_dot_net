using Domain.Exception;
using Domain.Interfaces.Room;

namespace Domain.Entities;

public class Ticket
{

    public int Id { get; set; }
    
    public string SeatName { get; set; }


    //aqui llamaremos la herramienta 
    public Ticket(string seatName, ISeatAvailabilityChecker checker )
    {

        if (!checker.IsAvailable(seatName))
        {
            throw new SeatAvailableException($"La silla {seatName} ya está ocupada.");
        }

        SeatName = seatName;
    }
    
}