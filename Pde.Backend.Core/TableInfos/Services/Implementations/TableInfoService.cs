using System.Collections.ObjectModel;
using Pde.Backend.Core.TableInfos.Contracts;
using Pde.Backend.Core.TableInfos.Models;
using Pde.Backend.Data.Database;
using Pde.Backend.Data.Models;

namespace Pde.Backend.Core.TableInfos.Services.Implementations;

public class TableInfoService : ITableInfoService
{
    private readonly IDbSchemaProvider _provider;

    public TableInfoService(IDbSchemaProvider provider)
    {
        _provider = provider;
    }

    /// <summary>
    ///     Get the schema of a database
    /// </summary>
    /// <param name="databaseConnectionInfo">The connection data needed to connect to the database.</param>
    /// <returns>A response object with a database object and a Result Enum</returns>
    public FetchDatabaseStructureResponse FetchDatabaseStructure(
        FetchDatabaseStructureRequest databaseConnectionInfo)
    {
        try
        {
            var tableColumnResponseList = _provider.FetchTablesAndColumns(
                databaseConnectionInfo.Username,
                databaseConnectionInfo.Password,
                databaseConnectionInfo.Host,
                databaseConnectionInfo.Port,
                databaseConnectionInfo.Database
            ).Result.ToList();

            var tableInfoList = MapToCollection(tableColumnResponseList);

            var databaseReturnObject = new DatabaseInfoViewModel
            {
                Name = databaseConnectionInfo.Database,
                Tables = tableInfoList
            };

            return new FetchDatabaseStructureResponse
            {
                DatabaseInfo = databaseReturnObject,
                Result = FetchDatabaseStructureResult.Success
            };
        }
        catch (Exception)
        {
            return new FetchDatabaseStructureResponse
            {
                DatabaseInfo = null,
                Result = FetchDatabaseStructureResult.ConnectionFailed
            };
        }
    }

    private static Collection<TableInfoViewModel> MapToCollection(List<TableColumnInfo> tableColumnList)
    {
        var tableInfoList = new Collection<TableInfoViewModel>();
        foreach (var result in tableColumnList)
        {
            var column = new ColumnInfoViewModel
            {
                Name = result.ColumnName,
                DataType = result.DataType
            };
            var foundTable = tableInfoList.FirstOrDefault(i => i.Name == result.TableName);
            if (foundTable != null)
            {
                foundTable.Columns.Add(column);
            }
            else
            {
                var table = new TableInfoViewModel
                {
                    Name = result.TableName,
                    Columns = new Collection<ColumnInfoViewModel>
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