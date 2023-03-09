namespace ConnectionService.Models;
public class TableInfo
{
    public string Name { get; set; }
    public List<ColumnInfo> Columns { get; set; }
    
    public bool Equals(TableInfo other)
    {
        if (other == null) return false;
        return Name.Equals(other.Name);
    }
}
