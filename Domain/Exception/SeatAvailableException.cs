namespace Domain.Exception;

public class SeatAvailableException: DomainException
{
    public SeatAvailableException(string seatName) : 
        base($"El Asiento {seatName} no esta disponible")
    {
    }
}