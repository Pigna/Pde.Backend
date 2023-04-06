using System.Data;

namespace Pde.Backend.Data.Database;

public interface IDbConnectionFactory
{
    IDbConnection Connect(string connectionString);

    string CreateConnectionString(string username, string password, string host, string port, string database);
}