using Dapper;
using Pde.Backend.Data.Models;

namespace Pde.Backend.Data.Database.Implementations;

public class PostgresSchemaProvider : IDbSchemaProvider
{
    private const string StrPostgresCommandTablesAndColumns = @"
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

    private const string StrPostgresCommandTableRelations = @"
        SELECT 
            cl2.relname as child_table,
	        att2.attname as child_column, 
            cl.relname as parent_table, 
            att.attname as parent_column,
            conname
        FROM
           (SELECT 
                unnest(con1.conkey) as parent, 
                unnest(con1.confkey) as child, 
                con1.confrelid, 
                con1.conrelid,
                con1.conname
            FROM 
                pg_class cl
                JOIN pg_namespace ns ON cl.relnamespace = ns.oid
                JOIN pg_constraint con1 ON con1.conrelid = cl.oid
            WHERE
	         con1.contype = 'f'
           ) con
           JOIN pg_attribute att ON
               att.attrelid = con.confrelid AND att.attnum = con.child
           JOIN pg_class cl ON
               cl.oid = con.confrelid
           JOIN pg_attribute att2 ON
               att2.attrelid = con.conrelid AND att2.attnum = con.parent
	        JOIN pg_class cl2 ON
	        cl2.oid = con.conrelid 
    ";

    private readonly IDbConnectionFactory _connectionFactory;

    public PostgresSchemaProvider(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<TableColumnInfo>> FetchTablesAndColumns(string username, string password, string host,
        string port, string database)
    {
        var connectionString = CreateConnectionString(username, password, host, port, database);

        using var databaseConnection = _connectionFactory.Connect(connectionString);
        var queryResult = await databaseConnection.QueryAsync<dynamic>(StrPostgresCommandTablesAndColumns);
        return queryResult.Select(item => new TableColumnInfo
        {
            TableName = item.table_name,
            ColumnName = item.column_name,
            DataType = item.data_type
        });
    }

    public async Task<IEnumerable<TableRelation>> FetchTablesRelations(string username, string password,
        string host,
        string port, string database)
    {
        var connectionString = CreateConnectionString(username, password, host, port, database);

        using var databaseConnection = _connectionFactory.Connect(connectionString);
        var queryResult = await databaseConnection.QueryAsync<dynamic>(StrPostgresCommandTableRelations);
        return queryResult.Select(item => new TableRelation
        {
            ChildTable = item.child_table,
            ChildColumn = item.child_column,
            ParentTable = item.parent_table,
            ParentColumn = item.parent_column,
            ConnectionName = item.conname
        });
    }

    private static string CreateConnectionString(string username, string password, string host, string port,
        string database)
    {
        return $@"
            User ID={username};
            Password={password};
            Host={host};
            Port={port};
            Database={database};
        ";
    }
}