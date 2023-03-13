using Pde.Backend.Core.TableInfos.Models;

namespace Pde.Backend.Core.TableInfos.Services;

public interface ITableInfoService
{
    List<TableInfoViewModel> GetTableInfo(DatabaseInfoViewModel databaseInfo);
}