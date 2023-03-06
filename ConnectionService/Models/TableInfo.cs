using Microsoft.EntityFrameworkCore;

namespace ConnectionService.Models;
[Keyless]
public class TableInfo
{
    public string Name { get; set; }
    public List<ColumnInfo> Columns { get; set; }
}
