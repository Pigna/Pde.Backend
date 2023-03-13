using System.Collections.ObjectModel;

namespace Pde.Backend.Core.TableInfos.Models;

public class TableInfoViewModel
{
    public string Name { get; set; } = null!;

    public Collection<ColumnInfoViewModel> Columns { get; set; } = null!;
}