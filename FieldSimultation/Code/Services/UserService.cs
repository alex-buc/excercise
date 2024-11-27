using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Code.Services;

public class UserService
{
    private readonly HttpClient _httpClient;

    // Constructor with dependency injection for HttpClient
    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // public async Task<User> GetUserAsync(string username, string password)
    // {
    //     var url = $"http://localhost:5171/api/user?user={username}&password={password}";

    //     // Send GET request to the API endpoint
    //     var response = await _httpClient.GetAsync(url);

    //     // Ensure the response is successful
    //     response.EnsureSuccessStatusCode();

    //     // Read the response content as a string
    //     var jsonResponse = await response.Content.ReadAsStringAsync();

    //     // Deserialize the JSON response into a User object
    //     var user = JsonSerializer.Deserialize<User>(jsonResponse);

    //     return user;
    // }
}