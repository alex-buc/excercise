using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using FieldSimultation.Code.Models;

namespace FieldSimultation.Code.Services;

public class MissionService
{
    private readonly HttpClient _httpClient = new HttpClient();

    public MissionService()
    {
        _httpClient.BaseAddress = new Uri($"{HttpClientConst.ApiGatewayUrl}/Missions/");
    }

    public async Task<List<MapShapeDto>?> getAllMapShapesByMissionId(int missionId) 
    {
        var response = await _httpClient.GetAsync($"{missionId}/map-shapes");
        if (response.IsSuccessStatusCode)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
            };
            var mapShapes = await response.Content.ReadFromJsonAsync<List<MapShapeDto>>(options);
            return mapShapes;
        }
        else
        {
            throw new Exception("Unable to retrieve map shapes data.");
        }
    }
}
