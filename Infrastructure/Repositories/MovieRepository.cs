using System.Data;
using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class MovieRepository : IMovieRepository
{
    
    private readonly DapperContext _context;
    
    public void Create(Movie movie) {
        using IDbConnection connection  = _context.CreateConnection();
        
        string sql = @"INSERT INTO Movie (Title, ReleaseDate, RoomId) 
                           VALUES (@Title, @ReleaseDate, @RoomId)";
        
        connection.Execute(sql, movie);

    }

    public Movie GetById(int id)
    {
        using IDbConnection connection = _context.CreateConnection();
        string sql = "SELECT * FROM Movie WHERE Id = @Id";
            
        // QueryFirstOrDefault trae el primer registro que encuentre, o nulo si no hay.
        return connection.QueryFirstOrDefault<Movie>(sql, new { Id = id });
        
        
    }

    public void Update(Movie movie)
    {
        using IDbConnection connection = _context.CreateConnection();
        string sql = @"UPDATE Movie 
                           SET Title = @Title, ReleaseDate = @ReleaseDate, RoomId = @RoomId 
                           WHERE Id = @Id";
        connection.Execute(sql, movie);
        
        
    }

    public void Delete(int id)
    {
        using IDbConnection connection = _context.CreateConnection();
        string sql = "DELETE FROM Movie WHERE Id = @Id";
        connection.Execute(sql, new { Id = id });    
        
    }

    public IEnumerable<Movie> GetByName(string name)
    {
        using IDbConnection connection = _context.CreateConnection();
        string sql = "SELECT * FROM Movie WHERE Title LIKE @Title";
        
        return connection.Query<Movie>(sql, new { Title = $"%{name}%" });
        
        
    }

    public IEnumerable<Movie> GetByReleaseDate(DateTime releaseDate)
    {
        using IDbConnection connection = _context.CreateConnection();

        string sql = "SELECT * FROM Movie WHERE CAST(ReleaseDate AS DATE) = @Date";

        return connection.Query<Movie>(sql, new { Date = releaseDate.Date });
    }
    
    public IEnumerable<Movie> GetMovies(bool? isActive = null)
    {
        using IDbConnection connection = _context.CreateConnection();

        string sql = "SELECT * FROM Movie";

        if (isActive.HasValue) 
        {
           
            sql += " WHERE IsActive = @Status";
        }
        
        return connection.Query<Movie>(sql, new { Status = isActive });
    }

    public string GetRoomStatus(string roomName)
    {
        using IDbConnection connection = _context.CreateConnection();

        string sql = @"SELECT COUNT(m.Id) 
                           FROM Movie m
                           INNER JOIN Room r ON m.RoomId = r.Id
                           WHERE r.Name = @RoomName";

        int movieCount = connection.ExecuteScalar<int>(sql, new { RoomName = roomName });

        if (movieCount < 3)
        {
            return "Sala disponible";
        }
        else if (movieCount >= 3 && movieCount <= 5)
        {
            return $"Sala con {movieCount} películas asignadas";
        }
        else
        {
            return "Sala no disponible";
        }
        
    }
}