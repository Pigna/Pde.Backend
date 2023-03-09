using ConnectionService.Models;
using Dapper;
using ConnectionInfo = ConnectionService.Models.ConnectionInfo;

namespace ConnectionService.Database;

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

    public async Task<IEnumerable<TableInfo>> GetTablesAndColumns(ConnectionInfo connectionInfo)
    {
        //"User ID=postgres;Password=postgrespw;Host=localhost;Port=32768;Database=postgres;"
        var connectionString = $@"
            User ID={connectionInfo.Username};
            Password={connectionInfo.Password};
            Host={connectionInfo.Host};
            Port={connectionInfo.Port};
            Database={connectionInfo.Database};
        ";
        using var database = _connectionFactory.Connect(connectionString);
        var queryResults = await database.QueryAsync<dynamic>(StrPostgresCommand);


        var tableInfoList = new List<TableInfo>();
        foreach (var result in queryResults)
        {
            var column = new ColumnInfo(result.column_name);
            var foundTable = tableInfoList.Find(i => i.Name == result.table_name);
            if (foundTable != null)
            {
                foundTable.Columns.Add(column);
            }
            else
            {
                var table = new TableInfo()
                {
                    Name = result.table_name,
                    Columns = new List<ColumnInfo>()
                    {
                        column
                    }
                };
                tableInfoList.Add(table);
            }
        }

        return tableInfoList;
    }
}