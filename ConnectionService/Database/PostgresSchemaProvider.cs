using ConnectionService.Models;
using Dapper;

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

    public async Task<IEnumerable<TableInfo>> GetTablesAndColumns()
    {
        using (var database = _connectionFactory.Connect())
        {
            var tables = await database.QueryAsync<dynamic>(StrPostgresCommand);


            List<TableInfo> tableInfoList = new List<TableInfo>();
            foreach (var result in tables)
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
}