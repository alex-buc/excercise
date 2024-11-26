using System.Text.Json.Serialization;

namespace ApiGateway.Models;

public class MissionAllocationViewModel 
{
    public int Id { get; set; }

    public StaffViewModel Staff { get; set; }

    public MissionViewModel Mission { get; set; }
}