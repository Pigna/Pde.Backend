using System.Data;

namespace ConnectionService.Database;

public interface IDbConnectionFactory
{
    IDbConnection Connect();
}