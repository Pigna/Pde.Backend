using Pde.Backend.Core.TableInfos.Contracts;
using Pde.Backend.Data.Database;

namespace Pde.Backend.Core.TableInfos.Services.Implementations;

public class ExportService : IExportService
{
    private readonly IDbExportProvider _provider;

    public ExportService(IDbExportProvider provider)
    {
        _provider = provider;
    }

    public SubmitExportResponse SubmitExportData(SubmitExportDataRequest request)
    {
        var combined = request.ExportDataViewModels.GroupBy(item => item.TableName).ToList();
        var tableData = new object();

        foreach (var table in combined)
        {
            var columns = table.Select(item => item.ColumnName).ToList();
            tableData = _provider.FetchTableData(
                table.Key,
                columns,
                request.DatabaseConnectionInfo.Username,
                request.DatabaseConnectionInfo.Password,
                request.DatabaseConnectionInfo.Host,
                request.DatabaseConnectionInfo.Port,
                request.DatabaseConnectionInfo.Database);
        }

        Console.WriteLine(tableData);
        //data invullen met bogus
        //data terug sturen
        return new SubmitExportResponse
        {
            Result = SubmitExportResult.Success
        };
    }
}