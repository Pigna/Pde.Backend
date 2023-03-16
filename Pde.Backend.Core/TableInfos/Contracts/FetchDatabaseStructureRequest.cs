namespace Pde.Backend.Core.TableInfos.Contracts;

public class FetchDatabaseStructureRequest
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Host { get; set; } = null!;
    public string Port { get; set; } = null!;
    public string Database { get; set; } = null!;
}