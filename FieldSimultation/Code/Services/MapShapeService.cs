using System;
using System.Net.Http;
using System.Threading.Tasks;
using FieldSimultation.Code.Models;

namespace FieldSimultation.Code.Services;

public class MapShapeService
{
    private readonly HttpClient _httpClient = new HttpClient();

    public MapShapeService() {
        _httpClient.BaseAddress = new Uri($"{HttpClientConst.ApiGatewayUrl}/MapShapes");
    }

    public async Task SaveMapShapeService(MapShapeDto mapShapeDto) {
        string url = $"?type={mapShapeDto.Type}&data={Uri.EscapeDataString(mapShapeDto.Data)}&missionId={mapShapeDto.MissionId}";
        url += (mapShapeDto.Id != null)?$"&id={mapShapeDto.Id}":"";
        var response = await _httpClient.PostAsync(url, null);
        if (!response.IsSuccessStatusCode)
        {
             throw new Exception("Unable to save map shapes data.");
        }
    }
}