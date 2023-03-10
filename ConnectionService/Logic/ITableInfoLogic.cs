using ConnectionService.Models;

namespace ConnectionService.Logic;

public interface ITableInfoLogic
{
    List<TableInfo> GetTableInfo(DatabaseInfo databaseInfo);
}