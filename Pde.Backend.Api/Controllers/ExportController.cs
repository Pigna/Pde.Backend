using Microsoft.AspNetCore.Mvc;
using Pde.Backend.Core.TableInfos.Contracts;
using Pde.Backend.Core.TableInfos.Services;

namespace Pde.Backend.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ExportController : ControllerBase
{
    private readonly IExportService _exportService;

    public ExportController(IExportService exportService)
    {
        _exportService = exportService;
    }

    [HttpPost]
    public async Task<OkObjectResult> SubmitExportData([FromBody] SubmitExportDataRequest request,
        CancellationToken cancellationToken)
    {
        return Ok(await _exportService.SubmitExportData(request));
    }
}