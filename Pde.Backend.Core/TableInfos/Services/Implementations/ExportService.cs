using Pde.Backend.Core.TableInfos.Contracts;

namespace Pde.Backend.Core.TableInfos.Services.Implementations;

public class ExportService : IExportService
{
    public SubmitExportResponse SubmitExportData(SubmitExportDataRequest request)
    {
        //db connectie maken
        //data ophalen
        //data invullen met bogus
        //data terug sturen
        Console.WriteLine("");
        return new SubmitExportResponse
        {
            Result = SubmitExportResult.Success
        };
    }
}