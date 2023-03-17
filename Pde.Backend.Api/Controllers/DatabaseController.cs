using Microsoft.AspNetCore.Mvc;
using Pde.Backend.Core.TableInfos.Contracts;
using Pde.Backend.Core.TableInfos.Services;

namespace Pde.Backend.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DatabaseController : ControllerBase
{
    private readonly ITableInfoService _tableInfoService;

    public DatabaseController(ITableInfoService tableInfoService)
    {
        _tableInfoService = tableInfoService;
    }

    [HttpPost]
    public async Task<IActionResult> FetchTables([FromBody] FetchDatabaseStructureRequest request,
        CancellationToken cancellationToken)
    {
        return Ok(_tableInfoService.FetchDatabaseStructure(request));
    }
}