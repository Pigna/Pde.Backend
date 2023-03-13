using Pde.Backend.Api.Models;

namespace PDE_Backend.Core.TableInfos.Contracts;

public class FetchDatabaseStructureResponse
{
    public DatabaseInfoViewModel? DatabaseInfo { get; set; }

    public FetchDatabaseStructureResult Result { get; set; }
}