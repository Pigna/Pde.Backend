using Pde.Backend.Core.TableInfos.Models;

namespace Pde.Backend.Core.TableInfos.Contracts;

public class FetchDatabaseStructureResponse
{
    public DatabaseInfoViewModel? DatabaseInfo { get; set; }

    public FetchDatabaseStructureResult Result { get; set; }
}