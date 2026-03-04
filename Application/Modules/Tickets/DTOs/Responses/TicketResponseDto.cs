namespace Application.Modules.Tickets.DTOs;

// Esta clase es solo una "bolsa de datos" para enviar a internet.
// No tiene lógica, no tiene constructores protegidos, es 100% pública.
public class TicketResponseDto
{
    public int TicketId { get; set; }
    public decimal Price { get; set; }
    public DateTime PurchaseDate { get; set; }
    //es decir el como 
    // En lugar de mandar el objeto entero, extraemos solo lo que el usuario quiere ver:
    public string CustomerName { get; set; }
    public string MovieTitle { get; set; }
    public string RoomName { get; set; }
}