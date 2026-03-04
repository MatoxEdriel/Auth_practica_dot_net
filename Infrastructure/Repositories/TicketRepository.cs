using System.Data;
using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class TicketRepository: ITicketRepository
{
    
    private readonly DapperContext _context;
    
    
    public TicketRepository(DapperContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Ticket> GetAll(TicketFilter filter)
    {
        using IDbConnection connection = _context.CreateConnection();
        var builder = new SqlBuilder();
        
        var template = builder.AddTemplate(@"
            SELECT 
                t.Id, t.MovieId, t.CustomerId, t.PurchaseDate, t.Price,
                
                c.Id, c.FullName, c.Email,
                
                m.Id, m.Title, m.ReleaseDate, m.RoomId,
                
                r.Id, r.Name
            FROM Ticket t
            INNER JOIN Customer c ON t.CustomerId = c.Id
            INNER JOIN Movie m ON t.MovieId = m.Id
            INNER JOIN Room r ON m.RoomId = r.Id
            /**where**/
        ");
        
        if (filter.MovieId.HasValue)
            builder.Where("t.MovieId = @MovieId", new { filter.MovieId });

        if (filter.CustomerId.HasValue)
            builder.Where("t.CustomerId = @CustomerId", new { filter.CustomerId });

        if (filter.StartDate.HasValue)
            builder.Where("CAST(t.PurchaseDate AS DATE) >= @StartDate", new { StartDate = filter.StartDate.Value.Date });

        if (filter.EndDate.HasValue)
            builder.Where("CAST(t.PurchaseDate AS DATE) <= @EndDate", new { EndDate = filter.EndDate.Value.Date });
        
        var tickets = connection.Query<Ticket, Customer, Movie, Room, Ticket>(
            sql: template.RawSql,
            map: (ticket, customer, movie, room) => 
            {
                // A. Metemos el Cliente dentro del Ticket
                ticket.Customer = customer; 
                
                // B. Metemos la Sala dentro de la Película
                movie.Room = room;          
                
                // C. Metemos la Película (que ya tiene su sala) dentro del Ticket
                ticket.Movie = movie;       

                // Devolvemos el Ticket completamente armado
                return ticket;              
            },
            param: template.Parameters,
            splitOn: "Id, Id, Id" 
        );

        return tickets;
    }
        
        
        
        
    
}