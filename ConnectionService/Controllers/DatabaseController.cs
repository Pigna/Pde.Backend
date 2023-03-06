using ConnectionService.Contexts;
using ConnectionService.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConnectionService.Controllers;

[ApiController]
[Route("[controller]")]
public class DatabaseController : ControllerBase
{

    private readonly ILogger<WeatherForecastController> _logger;

    public DatabaseController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TableInfo>>> GetTables()
    {
        var tables = new List<TableInfo>() { };
        foreach (var table in tables)
        {
            Console.Out.WriteLine(table.Name);
        }
        return tables;
        
    }
}