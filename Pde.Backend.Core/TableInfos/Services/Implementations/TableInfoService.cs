using System.Collections.ObjectModel;
using Pde.Backend.Core.TableInfos.Models;
using Pde.Backend.Data.Database;

namespace Pde.Backend.Core.TableInfos.Services.Implementations;

public class TableInfoService : ITableInfoService
{
    private readonly IDbSchemaProvider _provider;

    public TableInfoService(IDbSchemaProvider provider)
    {
        _provider = provider;
    }

    public List<TableInfoViewModel> GetTableInfo(DatabaseInfo databaseInfo)
    {
        var tableColumnList = _provider.GetTablesAndColumns(databaseInfo).Result.ToList();
        var tableInfoList = new List<TableInfoViewModel>();
        foreach (var result in tableColumnList)
        {
            var column = new ColumnInfoViewModel()
            {
                Name = result.ColumnName
            };
            var foundTable = tableInfoList.Find(i => i.Name == result.TableName);
            if (foundTable != null)
            {
                foundTable.Columns.Add(column);
            }
            else
            {
                var table = new TableInfoViewModel()
                {
                    Name = result.TableName,
                    Columns = new Collection<ColumnInfoViewModel>()
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