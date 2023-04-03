namespace Pde.Backend.Data.Models;

public class TableRelation
{
    public string ForeignKeyTable { get; set; } = null!;
    public string ForeignKeyColumn { get; set; } = null!;
    public string ForeignKeyConstraintName { get; set; } = null!;
    public string PrimaryKeyTable { get; set; } = null!;
    public string PrimaryKeyColumn { get; set; } = null!;
    public string PrimaryKeyConstraintName { get; set; } = null!;
}