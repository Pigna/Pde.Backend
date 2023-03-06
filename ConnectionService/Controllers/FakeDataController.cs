using Microsoft.AspNetCore.Mvc;

namespace ConnectionService.Controllers;

[ApiController]
[Route("[controller]")]
public class FakeDataController : ControllerBase
{
    private readonly ILogger<FakeDataController> _logger;

    public FakeDataController(ILogger<FakeDataController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "FakeData")]
    public FakeData Get()
    {
        return new FakeData()
        {
            value = Faker.Name.First()
        };
    }
}