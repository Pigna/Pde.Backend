using Pde.Backend.Api.Models;

namespace Pde.Backend.Core.Logic;

public interface ITableInfoLogic
{
    List<TableInfo> GetTableInfo(DatabaseInfo databaseInfo);
}