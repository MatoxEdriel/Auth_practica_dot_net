namespace Domain.Exception;

public class RoomFullException: DomainException
{
    public RoomFullException(string roomName) : 
        base($"La Sala {roomName} no tiene mas asientos disponibles")
    {
    }
}