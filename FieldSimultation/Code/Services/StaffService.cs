using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using FieldSimultation.Code.Models;

namespace FieldSimultation.Code.Services;

public class StaffService
{
    private readonly HttpClient _httpClient = new HttpClient();

    public StaffService()
    {
        _httpClient.BaseAddress = new Uri($"{HttpClientConst.ApiGatewayUrl}/Staffs/");
    }

     public async Task<List<MissionDto>> getAllMissionsByStuffId(int userId)
    {
        var response = await _httpClient.GetAsync($"{userId}/missions");
        if (response.IsSuccessStatusCode)
        {
            var missions = await response.Content.ReadFromJsonAsync<List<MissionDto>>();
            return missions;
        }
        else
        {
            throw new Exception("Unable to retrieve missions data.");
        }
    }
}
