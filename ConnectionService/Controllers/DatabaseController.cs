using ConnectionService.Logic;
using ConnectionService.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConnectionService.Controllers;

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
    public async Task<ActionResult<IEnumerable<TableInfo>>> GetTables()
    {
        //TODO: Get this information from header? or create session?
        var databaseInfo = new DatabaseInfo("postgres", "postgrespw", "localhost", "32768", "postgres");
        return Ok(_tableInfoLogic.GetTableInfo(databaseInfo));
    }
}