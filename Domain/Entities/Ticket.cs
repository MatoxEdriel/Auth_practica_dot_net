using Domain.Exception;
using Domain.Interfaces.Room;

namespace Domain.Entities;

public class Ticket
{

    public int Id { get; set; }
    public int MovieId { get; private set; }
    public int CustomerId { get; private set; }
    public DateTime PurchaseDate { get; private set; }
    public decimal Price { get; private set; } 
    
    //---------------------
    /*
     *Con el objetivo de mostrar informacion relacionada
     * de otras entidades/tablas 
     *
     */
    public Movie Movie { get; set; }
    public Customer Customer { get; set; }
    
    
    
    public string SeatName { get; set; }
    
    protected Ticket() { }  
    
    


    //aqui llamaremos la herramienta 
    
    /*
    public Ticket(string seatName, ISeatAvailabilityChecker checker )
    {

        if (!checker.IsAvailable(seatName))
        {
            throw new SeatAvailableException($"La silla {seatName} ya está ocupada.");
        }

        SeatName = seatName;
    }
    */
    
    public Ticket(int movieId, int customerId, DateTime purchaseDate, decimal price)
    {
        MovieId = movieId;
        CustomerId = customerId;
        PurchaseDate = purchaseDate;
        Price = price;
    }
 
    
}