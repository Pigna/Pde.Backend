using Pde.Backend.Core.TableInfos.Contracts;

namespace Pde.Backend.Core.TableInfos.Services;

public interface IExportService
{
    SubmitExportResponse SubmitExportData(SubmitExportDataRequest request);
}