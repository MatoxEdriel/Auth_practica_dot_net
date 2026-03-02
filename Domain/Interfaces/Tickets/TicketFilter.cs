namespace Domain.Interfaces;

// Esta clase agrupa los filtros de búsqueda. 
// Usamos el signo de interrogación (?) porque todos son opcionales.
// Si el usuario no manda un filtro, será null y lo ignoraremos.
public class TicketFilter
{
    public int? MovieId {get; set; }
    public int? CustomerId {get; set; }
    public DateTime? StartDate {get; set; }
    public DateTime? EndDate {get; set; }
}

