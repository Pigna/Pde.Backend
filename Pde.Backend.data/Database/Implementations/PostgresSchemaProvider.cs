using Dapper;
using Pde.Backend.Data.Models;

namespace Pde.Backend.Data.Database.Implementations;

public class PostgresSchemaProvider : IDbSchemaProvider
{
    private const string StrPostgresCommand = @"
        SELECT 
            table_name,
            column_name,
            data_type
        FROM 
            information_schema.columns
        WHERE 
            table_schema = 'public'
        ORDER BY
            table_name,
            ordinal_position;
    ";

    private readonly IDbConnectionFactory _connectionFactory;

    public PostgresSchemaProvider(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<TableColumnInfo>> FetchTablesAndColumns(string username, string password, string host,
        string port, string database)
    {
        var connectionString = $@"
            User ID={username};
            Password={password};
            Host={host};
            Port={port};
            Database={database};
        ";
        using var databaseConnection = _connectionFactory.Connect(connectionString);
        var queryResult = await databaseConnection.QueryAsync<dynamic>(StrPostgresCommand);
        return queryResult.Select(item => new TableColumnInfo
        {
            TableName = item.table_name,
            ColumnName = item.column_name,
            DataType = item.data_type
        });
    }
}