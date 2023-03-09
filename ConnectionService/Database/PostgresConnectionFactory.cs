using System.Data;
using Npgsql;

namespace ConnectionService.Database;

public class PostgresConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public PostgresConnectionFactory()
    {
        _connectionString = "User ID=postgres;Password=postgrespw;Host=localhost;Port=32768;Database=postgres;";
    }

    public PostgresConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection Connect()
    {
        return new NpgsqlConnection(_connectionString);
    }
}