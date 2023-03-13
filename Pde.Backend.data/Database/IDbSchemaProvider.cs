namespace Pde.Backend.Data.Database;

public interface IDbSchemaProvider
{
    public Task<IEnumerable<TableColumnDto>> GetTablesAndColumns(DatabaseInfo databaseInfo);
}