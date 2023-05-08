using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Pde.Backend.Core.TableInfos.Contracts;

namespace Pde.Backend.Core.TableInfos.Services.Implementations;

public class HttpExportService : IExportService
{
    private readonly HttpClient _httpClient;

    public HttpExportService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:5166/");
    }

    public async Task<SubmitExportResponse> SubmitExportData(SubmitExportDataRequest request)
    {
        var content = new StringContent(JsonSerializer.Serialize(request),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);
        using var httpResponseMessage = await _httpClient.PostAsync("/HangfireJob/", content);

        var response = new SubmitExportResponse
        {
            Result = SubmitExportResult.ConnectionFailed
        };

        if (httpResponseMessage.EnsureSuccessStatusCode().IsSuccessStatusCode)
            response.Result = SubmitExportResult.Success;
        return response;
    }
}