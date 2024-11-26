using System.Text.Json.Serialization;

namespace ApiGateway.Models;

public class MissionViewModel 
{
    public int Id { get; set; }

    public string FullName  { get; set; }

    public double StartPositionLat { get; set; }

    public double StartPositionLng { get; set; }
}
