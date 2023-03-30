namespace Pde.Backend.Core.TableInfos.Models;

public class ColumnInfoViewModel
{
    public string Name { get; set; } = null!;

    public string DataType { get; set; } = null!;
    public string? Default { get; set; } = null!;
    public bool IsNullable { get; set; }
    public ICollection<ConstraintViewModel> Constraints { get; set; }
}