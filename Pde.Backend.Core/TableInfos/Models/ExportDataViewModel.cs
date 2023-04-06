namespace Pde.Backend.Core.TableInfos.Models;

public class ExportDataViewModel
{
    public string TableName { get; set; } = null!;
    public string ColumnName { get; set; } = null!;
    public FakeDataType DataType { get; set; }
}