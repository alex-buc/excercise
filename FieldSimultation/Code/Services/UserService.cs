using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FieldSimultation.Code.Models;

namespace FieldSimultation.Code.Services;

public class UserService
{
    private readonly HttpClient _httpClient = new HttpClient();

    public UserService()
    {
        _httpClient.BaseAddress = new Uri($"{HttpClientConst.ApiGatewayUrl}/Users/");
    }

    public async Task<UserDto> GetUserAsync(string username, string password)
    {
        var response = await _httpClient.GetAsync($"authentificate?user={username}&password={password}");
        if (response.IsSuccessStatusCode)
        {
            var user = await response.Content.ReadFromJsonAsync<UserDto>();
            return user;
        }
        else
        {
            throw new Exception("Unable to retrieve user data.");
        }
    }

    public async Task<StaffDto> GetStaffInfoAsync(int userId)
    {
        var response = await _httpClient.GetAsync($"{userId}/staff");
        if (response.IsSuccessStatusCode)
        {
            var staff = await response.Content.ReadFromJsonAsync<StaffDto>();
            return staff;
        }
        else
        {
            throw new Exception("Unable to retrieve staff data.");
        }
    }
}