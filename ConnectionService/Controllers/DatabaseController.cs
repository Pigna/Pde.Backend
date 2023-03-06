using ConnectionService.Contexts;
using ConnectionService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
        await using (var context = new EfDbContext())
        {
            var tables = context.Model.GetEntityTypes()
                .Where(t => t.BaseType == null)
                .Select(t => new TableInfo 
                {
                    Name = t.GetTableName(),
                    Columns = t.GetProperties().Select(p => new ColumnInfo 
                    {
                        Name = p.Name,
                        Type = p.ClrType.Name
                    }).ToList()
                }).ToList();
            foreach (var table in tables)
            {
                Console.Out.WriteLine(table.Name);
            }
            return tables;
        }
    }
}