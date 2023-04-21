using Pde.Backend.Core.TableInfos.Contracts;
using Pde.Backend.Core.TableInfos.Models;

namespace Pde.Backend.Core.TableInfos.Services;

public interface IFakeDataService
{
    FetchFakeDataResponse FetchFakeData(FetchFakeDataRequest request);
    object GetFakerDataByType(FakeDataType requestType, int seed);
}