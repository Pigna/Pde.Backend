using System.Data;
using Npgsql;

namespace Pde.Backend.Data.Database.Implementations;

public class PostgresConnectionFactory : IDbConnectionFactory
{
    public IDbConnection Connect(string connectionString)
    {
        return new NpgsqlConnection(connectionString);
    }
}