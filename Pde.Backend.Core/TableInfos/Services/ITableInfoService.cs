using Pde.Backend.Core.TableInfos.Contracts;

namespace Pde.Backend.Core.TableInfos.Services;

public interface ITableInfoService
{
    FetchDatabaseStructureResponse FetchDatabaseStructure(FetchDatabaseStructureRequest databaseConnectionInfo);
}