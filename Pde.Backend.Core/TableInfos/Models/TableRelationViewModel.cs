namespace Pde.Backend.Core.TableInfos.Models;

public class TableRelationViewModel
{
    public string ForeignKeyTable { get; set; }
    public string ForeignKeyColumn { get; set; }

    public string ForeignKeyConstraintName { get; set; }
    public string PrimaryKeyTable { get; set; }
    public string PrimaryKeyColumn { get; set; }
    public string PrimaryKeyConstraintName { get; set; }
}