using ConnectionService.Database;
using ConnectionService.Models;
using Microsoft.AspNetCore.Mvc;

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
        var tables = new List<TableInfo>() { };
        tables.AddRange(_provider.GetTablesAndColumns().Result.ToList());
        return Ok(tables);
        
    }
}