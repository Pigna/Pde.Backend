namespace Pde.Backend.Data.Database;

public interface IDbExportProvider
{
    object FetchTableData(string tableName, ICollection<string> columns,
        string username,
        string password,
        string host,
        string port,
        string database);
}