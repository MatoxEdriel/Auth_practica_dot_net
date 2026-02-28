namespace Domain.Interfaces.Room;

//Ejemplod einterface de ejemplo en una clase room pues se necesita saber si la silla esta usada o no 
//entonces esa validacion la puedo hacer desde domain 
public interface ISeatAvailabilityChecker
{
    bool IsAvailable(string seatNumber);    
}