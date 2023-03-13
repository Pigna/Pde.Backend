using Dapper;

namespace Pde.Backend.Data.Database.Implementations;

public class PostgresSchemaProvider : IDbSchemaProvider
{
    private readonly IDbConnectionFactory _connectionFactory;

    private const string StrPostgresCommand = @"
        SELECT 
            table_name,
            column_name
        FROM 
            information_schema.columns
        WHERE 
            table_schema = 'public'
        ORDER BY
            table_name,
            ordinal_position;
    ";

    public PostgresSchemaProvider(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<TableColumnDto>> GetTablesAndColumns(DatabaseInfo databaseInfo)
    {
        var connectionString = $@"
            User ID={databaseInfo.Username};
            Password={databaseInfo.Password};
            Host={databaseInfo.Host};
            Port={databaseInfo.Port};
            Database={databaseInfo.Database};
        ";
        using var database = _connectionFactory.Connect(connectionString);
        var queryResult = await database.QueryAsync<dynamic>(StrPostgresCommand);
        return queryResult.Select(item => new TableColumnDto()
        {
            TableName = item.table_name,
            ColumnName = item.column_name
        }).ToList();
    }
}