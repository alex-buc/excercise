using System.Text.Json.Serialization;

namespace ApiGateway.Models;

public class MapShapeViewModel 
{
    public int Id { get; set; }

    public string Type { get; set; }

    public string Data { get; set; }

    public StaffViewModel Staff { get; set; }
}

