
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
namespace Infrastructure.Data;

public class DapperContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DBPractice"); 
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}