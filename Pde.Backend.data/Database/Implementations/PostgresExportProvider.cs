namespace Pde.Backend.Data.Database.Implementations;

public class PostgresExportProvider : IDbExportProvider
{
    public object FetchTableData(string tableName, ICollection<string> columns,
        string username,
        string password,
        string host,
        string port,
        string database)
    {
        Console.WriteLine(asd(tableName, columns));
        return null;
    }

    private string asd(string tableName, ICollection<string> columns)
    {
        //TODO: How does this handle a list>?
        var selectQuery = @$"SELECT {columns} FROM {tableName} ";
        return selectQuery;
    }
}