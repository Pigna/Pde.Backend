using System.Collections.ObjectModel;
using Dapper;
using Pde.Backend.Data.Models;

namespace Pde.Backend.Data.Database.Implementations;

public class PostgresSchemaProvider : IDbSchemaProvider
{
    private const string StrPostgresCommandFetchTablesColumns = @"
        SELECT 
            col.table_name,
            col.column_name,
            col.data_type,
            col.column_default,
            col.is_nullable,
            us.constraint_name,
            con.constraint_type
        FROM 
	        information_schema.columns AS col
	        LEFT JOIN information_schema.key_column_usage AS us
		        ON col.table_name = us.table_name
		        AND col.column_name = us.column_name
	        LEFT JOIN information_schema.table_constraints AS con
		        ON us.constraint_name = con.constraint_name
        WHERE col.table_schema = 'public'
        ORDER BY col.table_name, col.ordinal_position;
";

    private const string easyselect = @"
        SELECT 
            main.conrelid as fkey_conrelid,
            main.conname as fkey,
            main.confrelid as fkey_confrelid,
            relation.conrelid as pkey_conrelid,
            relation.conname as pkey
	    FROM
	       pg_constraint main
	    JOIN pg_constraint relation on main.confrelid = relation.conrelid
	    where relation.contype = 'p'
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

    public async Task<IEnumerable<TableColumnInfo>> FetchTablesAndColumns(string username, string password,
        string host,
        string port, string database)
    {
        var connectionString = CreateConnectionString(username, password, host, port, database);

        using var databaseConnection = _connectionFactory.Connect(connectionString);
        var queryResult = (await databaseConnection.QueryAsync<dynamic>(StrPostgresCommandFetchTablesColumns)).ToList();

        var mappedResults = new Collection<TableColumnInfo>();

        var tables = queryResult.GroupBy(r => r.table_name).ToList();

        //Loop the grouped tables
        foreach (var table in tables)
        {
            string tableName = table.Key;

            var columns = table.GroupBy(t => t.column_name).ToList();

            //Loop the columns
            foreach (var column in columns)
            {
                string columnName = column.Key;
                string dataType = column.First().data_type;
                string columnDefault = column.First().column_default;
                bool isNullable = column.First().is_nullable == "YES";

                var constraints = new Collection<Constraint>();

                //Loop over self to get constraints
                foreach (var item in column)
                    if (item.constraint_name != null)
                        constraints.Add(new Constraint
                        {
                            Name = item.constraint_name,
                            Type = item.constraint_type
                        });

                mappedResults.Add(new TableColumnInfo
                {
                    TableName = tableName,
                    ColumnName = columnName,
                    DataType = dataType,
                    ColumnDefault = columnDefault,
                    Constraints = constraints,
                    IsNullable = isNullable
                });
            }
        }

        return mappedResults;
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