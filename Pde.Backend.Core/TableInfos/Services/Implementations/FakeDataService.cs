using Bogus;
using Pde.Backend.Core.TableInfos.Contracts;
using Pde.Backend.Core.TableInfos.Models;

namespace Pde.Backend.Core.TableInfos.Services.Implementations;

public class FakeDataService : IFakeDataService
{
    /// <summary>
    ///     Generate fake data according to the supplied Request
    /// </summary>
    /// <param name="request">FetchFakeDataRequest filled with FakeDataType</param>
    /// <returns>FetchFakeDataResponse with Result as Success or Failed and Value as the requested data. </returns>
    public FetchFakeDataResponse FetchFakeData(FetchFakeDataRequest request)
    {
        var faker = new Faker();
        var response = new FetchFakeDataResponse
        {
            Result = FetchFakeDataResult.Success
        };
        switch (request.Type)
        {
            case FakeDataType.Account:
                response.Value = faker.Finance.Account();
                break;
            case FakeDataType.Amount:
                response.Value = faker.Finance.Amount();
                break;
            case FakeDataType.Avatar:
                //TODO: Avatar or profile picture
                break;
            case FakeDataType.BuildingNumber:
                response.Value = faker.Address.BuildingNumber();
                break;
            case FakeDataType.City:
                response.Value = faker.Address.City();
                break;
            case FakeDataType.Country:
                response.Value = faker.Address.Country();
                break;
            case FakeDataType.CreditCardNumber:
                response.Value = faker.Finance.CreditCardNumber();
                break;
            case FakeDataType.DateFuture:
                response.Value = faker.Date.Future();
                break;
            case FakeDataType.DatePast:
                response.Value = faker.Date.Past();
                break;
            case FakeDataType.Email:
                response.Value = faker.Internet.Email();
                break;
            case FakeDataType.FirstName:
                response.Value = faker.Name.FirstName();
                break;
            case FakeDataType.FullAddress:
                response.Value = faker.Address.FullAddress();
                break;
            case FakeDataType.FullName:
                response.Value = faker.Name.FullName();
                break;
            case FakeDataType.Iban:
                response.Value = faker.Finance.Iban();
                break;
            case FakeDataType.LastName:
                response.Value = faker.Name.LastName();
                break;
            case FakeDataType.Latitude:
                response.Value = faker.Address.Latitude();
                break;
            case FakeDataType.Longitude:
                response.Value = faker.Address.Longitude();
                break;
            case FakeDataType.Month:
                response.Value = faker.Date.Month();
                break;
            case FakeDataType.Password:
                response.Value = faker.Internet.Password();
                break;
            case FakeDataType.PhoneNumber:
                response.Value = faker.Phone.PhoneNumber();
                break;
            case FakeDataType.Picture:
                response.Value = faker.Image.PicsumUrl();
                break;
            case FakeDataType.State:
                response.Value = faker.Address.State();
                break;
            case FakeDataType.StreetAddress:
                response.Value = faker.Address.StreetAddress();
                break;
            case FakeDataType.StreetName:
                response.Value = faker.Address.StreetName();
                break;
            case FakeDataType.UserName:
                response.Value = faker.Internet.UserName();
                break;
            case FakeDataType.Zipcode:
                response.Value = faker.Address.ZipCode();
                break;
            default:
                response.Result = FetchFakeDataResult.Fail;
                break;
        }

        return response;
    }
}