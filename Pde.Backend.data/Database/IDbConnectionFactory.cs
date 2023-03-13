using System.Data;

namespace Pde.Backend.Data.Database;

public interface IDbConnectionFactory
{
    IDbConnection Connect(string connectionString);
}