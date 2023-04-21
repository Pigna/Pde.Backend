using Pde.Backend.Data.Models;

namespace Pde.Backend.Core.TableInfos.Contracts;

public class SubmitExportResponse
{
    public SubmitExportResult Result { get; set; }
    public IList<TableData>? Value { get; set; }
}