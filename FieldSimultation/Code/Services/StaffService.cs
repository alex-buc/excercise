using System;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Code.Models;

namespace Code.Services;

public class StaffService
{
    private readonly HttpClient _httpClient = new HttpClient();

    public StaffService()
    {
        _httpClient.BaseAddress = new Uri($"{HttpClientConst.ApiGatewayUrl}/Staff/");
    }

    // public async Task<StaffDto> GetUserAsync(string username, string password)
    // {
    //     using (var response = await _httpClient.GetAsync($"user?user={username}&password={password}")) 
    //     {
    //         if (response.IsSuccessStatusCode)
    //         {
    //             var user = await response.Content.ReadFromJsonAsync<UserDto>();
    //             return user;
    //         }
    //         else
    //         {
    //             throw new Exception("Unable to retrieve user data.");
    //         }
    //     }
    // }
}
