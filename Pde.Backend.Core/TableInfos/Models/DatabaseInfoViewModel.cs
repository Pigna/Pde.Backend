using System.Collections.ObjectModel;

namespace Pde.Backend.Api.Models;

public class DatabaseInfoViewModel
{
    public string Name { get; set; }

    public Collection<TableInfoViewModel> Tables { get; set; }
}