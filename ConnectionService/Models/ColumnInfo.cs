using Microsoft.EntityFrameworkCore;

namespace ConnectionService.Models;
[Keyless]
public class ColumnInfo
{
    public string Name { get; set; }
    public string Type { get; set; }
}