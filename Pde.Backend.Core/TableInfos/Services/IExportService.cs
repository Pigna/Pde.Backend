using Pde.Backend.Core.TableInfos.Contracts;

namespace Pde.Backend.Core.TableInfos.Services;

public interface IExportService
{
    Task<SubmitExportResponse> SubmitExportData(SubmitExportDataRequest request);
}