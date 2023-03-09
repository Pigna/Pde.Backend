using ConnectionService.Models;
using ConnectionInfo = ConnectionService.Models.ConnectionInfo;

namespace ConnectionService.Database;

public interface IDbSchemaProvider
{
    public Task<IEnumerable<TableInfo>> GetTablesAndColumns(ConnectionInfo connectionInfo);
}