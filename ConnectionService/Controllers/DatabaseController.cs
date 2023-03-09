using ConnectionService.Database;
using ConnectionService.Models;
using Microsoft.AspNetCore.Mvc;
using ConnectionInfo = ConnectionService.Models.ConnectionInfo;

namespace ConnectionService.Controllers;

[ApiController]
[Route("[controller]")]
public class DatabaseController : ControllerBase
{
    private readonly IDbSchemaProvider _provider;
    
    public DatabaseController (IDbSchemaProvider provider)
    {
        _provider = provider;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TableInfo>>> GetTables()
    {
        var info = new ConnectionInfo("postgres", "postgrespw", "localhost", "32768", "postgres");
        var tables = new List<TableInfo>() { };
        tables.AddRange(_provider.GetTablesAndColumns(info).Result.ToList());
        return Ok(tables);
        
    }
}