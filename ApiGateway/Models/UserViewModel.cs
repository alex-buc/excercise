using System.Text.Json.Serialization;

namespace ApiGateway.Models;

public class UserViewModel 
{
    public int Id { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }
}