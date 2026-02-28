using Dapper;
using Domain.Interfaces.Room;
using Infrastructure.Data;

namespace Infrastructure.Interfaces;

public class SqlSeatAvailabilityChecker:ISeatAvailabilityChecker
{
    
    private readonly DapperContext _context;
    
    public SqlSeatAvailabilityChecker(DapperContext context) { _context = context; }
    public bool IsAvailable(string seatNumber)
    {
        using var connection = _context.CreateConnection();
        int count = connection.ExecuteScalar<int>(
            "SELECT COUNT(*) FROM Tickets WHERE SeatNumber = @Seat", new { Seat = seatNumber });
            
        return count == 0; 
    }
}