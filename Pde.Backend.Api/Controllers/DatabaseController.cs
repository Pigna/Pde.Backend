using Microsoft.AspNetCore.Mvc;
using Pde.Backend.Core.TableInfos.Models;
using Pde.Backend.Core.TableInfos.Services;

namespace Pde.Backend.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DatabaseController : ControllerBase
{
    private readonly ITableInfoService _tableInfoLogic;

    public DatabaseController(ITableInfoService tableInfoLogic)
    {
        _tableInfoLogic = tableInfoLogic;
    }

    [HttpGet]
    public async Task<IActionResult> GetTables()
    {
        //TODO: Get this information from header? or recreate to post
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