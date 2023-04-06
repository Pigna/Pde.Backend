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

    private const string StrPostgresCommandTableRelations = @"
        SELECT 
	        --foreign key
	        conf.table_name AS fk_table,
	        conf.column_name AS fk_column,
	        ref.constraint_name AS fk_constraint_name,
	        --prmary key
	        conp.table_name AS pk_table,
	        conp.column_name AS pk_column,
	        ref.unique_constraint_name AS pk_constraint_name,
	        ref.constraint_name || ref.unique_constraint_name as X
        FROM
	        information_schema.REFERENTIAL_CONSTRAINTS ref
        JOIN
	        information_schema.key_column_usage conf ON ref.constraint_name = conf.constraint_name
        JOIN
	        information_schema.key_column_usage conp ON ref.unique_constraint_name = conp.constraint_name
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
        var connectionString = _connectionFactory.CreateConnectionString(username, password, host, port, database);

        using var databaseConnection = _connectionFactory.Connect(connectionString);
        var queryResult = await databaseConnection.QueryAsync<dynamic>(StrPostgresCommandTableRelations);
        return queryResult.Select(item => new TableRelation
        {
            ForeignKeyTable = item.fk_table,
            ForeignKeyColumn = item.fk_column,
            ForeignKeyConstraintName = item.fk_constraint_name,
            PrimaryKeyTable = item.pk_table,
            PrimaryKeyColumn = item.pk_column,
            PrimaryKeyConstraintName = item.pk_constraint_name
        });
    }

    public async Task<IEnumerable<TableColumnInfo>> FetchTablesAndColumns(string username, string password,
        string host,
        string port, string database)
    {
        var connectionString = _connectionFactory.CreateConnectionString(username, password, host, port, database);

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
}