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
            //Tables and Columns
            var tableColumnResponseList = _provider.FetchTablesAndColumns(
                databaseConnectionInfo.Username,
                databaseConnectionInfo.Password,
                databaseConnectionInfo.Host,
                databaseConnectionInfo.Port,
                databaseConnectionInfo.Database
            ).Result.ToList();

            var tableInfoList = MapToTableInfoViewModelCollection(tableColumnResponseList);

            //Table Relations
            var relationsResponseList = _provider.FetchTablesRelations(
                databaseConnectionInfo.Username,
                databaseConnectionInfo.Password,
                databaseConnectionInfo.Host,
                databaseConnectionInfo.Port,
                databaseConnectionInfo.Database
            ).Result.ToList();

            var relationList = MapToTableRelationViewModelsCollection(relationsResponseList);

            var databaseReturnObject = new DatabaseInfoViewModel
            {
                Name = databaseConnectionInfo.Database,
                Relations = relationList,
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

    /// <summary>
    ///     Loop over the database results and parse to a TableInfoViewModel collection
    /// </summary>
    /// <param name="tableColumnList">List of TableColumnInfo filled with data from the database</param>
    /// <returns>A mapped object of TableInfo</returns>
    private static Collection<TableInfoViewModel> MapToTableInfoViewModelCollection(
        List<TableColumnInfo> tableColumnList)
    {
        var tableInfoList = new Collection<TableInfoViewModel>();
        foreach (var result in tableColumnList)
        {
            var column = new ColumnInfoViewModel
            {
                Name = result.ColumnName,
                DataType = result.DataType,
                Default = result.ColumnDefault,
                IsNullable = result.IsNullable,
                Constraints = MapConstraintViewModel(result.Constraints)
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

    private static Collection<TableRelationViewModel> MapToTableRelationViewModelsCollection(
        List<TableRelation> tableRelation)
    {
        var mappedList = new Collection<TableRelationViewModel>();

        foreach (var item in tableRelation)
            mappedList.Add(new TableRelationViewModel
            {
                ForeignKeyTable = item.ForeignKeyTable,
                ForeignKeyColumn = item.ForeignKeyColumn,
                ForeignKeyConstraintName = item.ForeignKeyConstraintName,
                PrimaryKeyTable = item.PrimaryKeyTable,
                PrimaryKeyColumn = item.PrimaryKeyColumn,
                PrimaryKeyConstraintName = item.PrimaryKeyConstraintName
            });

        return mappedList;
    }

    private static Collection<ConstraintViewModel> MapConstraintViewModel(ICollection<Constraint> constraints)
    {
        var mappedConstraints = new Collection<ConstraintViewModel>();
        foreach (var constraint in constraints)
        {
            var constraintViewModel = new ConstraintViewModel
            {
                Name = constraint.Name
            };

            if (constraint.Type == "UNIQUE")
                constraintViewModel.Type = ConstraintType.Unique;
            else if (constraint.Type == "PRIMARY KEY")
                constraintViewModel.Type = ConstraintType.Primary;
            else if (constraint.Type == "FOREIGN KEY")
                constraintViewModel.Type = ConstraintType.Foreign;
            else if (constraint.Type == "CHECK")
                constraintViewModel.Type = ConstraintType.Check;
            mappedConstraints.Add(constraintViewModel);
        }

        return mappedConstraints;
    }
}