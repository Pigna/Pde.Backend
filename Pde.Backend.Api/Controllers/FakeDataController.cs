using Microsoft.AspNetCore.Mvc;
using Pde.Backend.Api.Models;

namespace Pde.Backend.Api.Controllers;

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
            Value = Faker.Name.First()
        };
    }
}