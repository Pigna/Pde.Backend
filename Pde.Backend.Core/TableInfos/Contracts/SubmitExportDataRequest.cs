using Pde.Backend.Core.TableInfos.Models;

namespace Pde.Backend.Core.TableInfos.Contracts;

public class SubmitExportDataRequest
{
    public DatabaseConnectionInfoViewModel DatabaseConnectionInfo { get; set; } = null!;
    public ICollection<ExportDataViewModel> ExportDataViewModels { get; set; } = null!;
}