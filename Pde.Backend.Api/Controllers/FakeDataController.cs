using Microsoft.AspNetCore.Mvc;
using Pde.Backend.Core.TableInfos.Contracts;
using Pde.Backend.Core.TableInfos.Services;

namespace Pde.Backend.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FakeDataController : ControllerBase
{
    private readonly IFakeDataService _fakeDataService;

    public FakeDataController(IFakeDataService fakeDataService)
    {
        _fakeDataService = fakeDataService;
    }

    [HttpPost]
    public async Task<OkObjectResult> FetchFakeData([FromBody] FetchFakeDataRequest request,
        CancellationToken cancellationToken)
    {
        return Ok(_fakeDataService.FetchFakeData(request));
    }
}