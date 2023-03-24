namespace Pde.Backend.Core.TableInfos.Models;

public class TableRelationViewModel
{
    public string ChildTable { get; set; }
    public string ChildColumn { get; set; }
    public string ParentTable { get; set; }
    public string ParentColumn { get; set; }
    public string ConnectionName { get; set; }
}