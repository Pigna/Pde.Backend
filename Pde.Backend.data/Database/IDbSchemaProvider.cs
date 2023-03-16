using Pde.Backend.Data.Models;

namespace Pde.Backend.Data.Database;

public interface IDbSchemaProvider
{
    public Task<IEnumerable<TableColumnInfo>> FetchTablesAndColumns(string username, string password, string host,
        string port,
        string database);
}