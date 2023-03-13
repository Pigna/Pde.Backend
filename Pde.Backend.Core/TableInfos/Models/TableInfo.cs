namespace Pde.Backend.Api.Models;

public class TableInfo
{
    public string Name { get; set; } = null!;
    public List<ColumnInfo> Columns { get; set; } = null!;
}