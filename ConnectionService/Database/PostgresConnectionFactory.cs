using System.Data;
using Npgsql;

namespace ConnectionService.Database;

public class PostgresConnectionFactory : IDbConnectionFactory
{
    public PostgresConnectionFactory()
    {
        
    }

    public IDbConnection Connect(string connectionString)
    {
        return new NpgsqlConnection(connectionString);
    }
}