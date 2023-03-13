using Microsoft.AspNetCore.Mvc;
using Pde.Backend.Api.Logic;
using Pde.Backend.Api.Models;

namespace Pde.Backend.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DatabaseController : ControllerBase
{
    private readonly ITableInfoLogic _tableInfoLogic;

    public DatabaseController(ITableInfoLogic tableInfoLogic)
    {
        _tableInfoLogic = tableInfoLogic;
    }

    [HttpGet]
    public async Task<IActionResult> GetTables()
    {
        //TODO: Get this information from header? or create session?
        var databaseInfo = new DatabaseInfo()
        {
            Username = "postgres",
            Password = "postgrespw",
            Host = "localhost",
            Port = "32768",
            Database = "postgres"
        };
        return Ok(_tableInfoLogic.GetTableInfo(databaseInfo));
    }
}