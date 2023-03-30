namespace Pde.Backend.Data.Models;

public class TableColumnInfo
{
    public string TableName { get; set; } = null!;
    public string ColumnName { get; set; } = null!;
    public string DataType { get; set; } = null!;
    public string? ColumnDefault { get; set; }
    public bool IsNullable { get; set; } = false;
    public ICollection<Constraint> Constraints { get; set; } = null!;
}