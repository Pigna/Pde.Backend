using Pde.Backend.Core.TableInfos.Models;

namespace Pde.Backend.Core.TableInfos.Contracts;

public class FetchFakeDataRequest
{
    public FakeDataType Type { get; set; }
}