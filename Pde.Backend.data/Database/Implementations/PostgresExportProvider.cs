using Dapper;

namespace Pde.Backend.Data.Database.Implementations;

public class PostgresExportProvider : IDbExportProvider
{
    private readonly IDbConnectionFactory _connectionFactory;

    public PostgresExportProvider(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<object> FetchTableData(
        string tableName,
        ICollection<string> columns,
        string username,
        string password,
        string host,
        string port,
        string database)
    {
        var sqlSelectQuery = CreateSqlQuery(tableName, columns);
        var connectionString = _connectionFactory.CreateConnectionString(username, password, host, port, database);
        using var databaseConnection = _connectionFactory.Connect(connectionString);
        var queryResult = await databaseConnection.QueryAsync<dynamic>(sqlSelectQuery);
        return queryResult;
    }

    private string CreateSqlQuery(string tableName, ICollection<string> columns)
    {
        var preparedColumns = string.Join(",", columns);
        var selectQuery = @$"SELECT {preparedColumns} FROM {tableName} ";
        return selectQuery;
    }
}