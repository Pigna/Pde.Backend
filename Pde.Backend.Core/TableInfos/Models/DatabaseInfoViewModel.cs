using System.Collections.ObjectModel;

namespace Pde.Backend.Core.TableInfos.Models;

public class DatabaseInfoViewModel
{
    public string Name { get; set; } = null!;

    public Collection<TableInfoViewModel> Tables { get; set; } = null!;
}