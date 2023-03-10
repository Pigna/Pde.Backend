namespace ConnectionService.Models;

public class TableInfo
{
    public string Name { get; set; } = null!;
    public List<ColumnInfo> Columns { get; set; } = null!;
}