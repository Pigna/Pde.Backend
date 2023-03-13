using System.Data;

namespace Pde.Backend.Data.Database.Implementations;

public class PostgresConnectionFactory : IDbConnectionFactory
{
    public IDbConnection Connect(string connectionString)
    {
        return new NpgsqlConnection(connectionString);
    }
}