using Pde.Backend.Core.TableInfos.Contracts;

namespace Pde.Backend.Core.TableInfos.Services;

public interface IFakeDataService
{
    FetchFakeDataResponse FetchFakeData(FetchFakeDataRequest request);
}