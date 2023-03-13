using Pde.Backend.Api.Database;
using Pde.Backend.Api.Models;

namespace Pde.Backend.Core.Logic;

public class TableInfoLogic : ITableInfoLogic
{
    private readonly IDbSchemaProvider _provider;

    public TableInfoLogic(IDbSchemaProvider provider)
    {
        _provider = provider;
    }

    public List<TableInfo> GetTableInfo(DatabaseInfo databaseInfo)
    {
        var tableColumnList = _provider.GetTablesAndColumns(databaseInfo).Result.ToList();
        var tableInfoList = new List<TableInfo>();
        foreach (var result in tableColumnList)
        {
            var column = new ColumnInfo()
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
                var table = new TableInfo()
                {
                    Name = result.TableName,
                    Columns = new List<ColumnInfo>()
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