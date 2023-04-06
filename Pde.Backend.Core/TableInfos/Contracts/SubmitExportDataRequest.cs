using Pde.Backend.Core.TableInfos.Models;

namespace Pde.Backend.Core.TableInfos.Contracts;

public class SubmitExportDataRequest
{
    public DatabaseInfoViewModel DatabaseInfo { get; set; } = null!;
    public ICollection<ExportDataViewModel> ExportDataViewModels { get; set; } = null!;
}