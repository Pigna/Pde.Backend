using System.Collections.ObjectModel;

namespace Pde.Backend.Api.Models;

public class TableInfoViewModel
{
    public string Name { get; set; }

    public Collection<ColumnInfoViewModel> Columns { get; set; }
}