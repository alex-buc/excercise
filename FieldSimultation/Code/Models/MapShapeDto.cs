namespace FieldSimultation.Code.Models;
public class MapShapeDto
{
    public int? Id { get; set; }
    public MarkerType Type { get; set; }

    public string Data { get; set; }

    public int MissionId { get; set; }
}

public enum MarkerType 
{
    OWN_LOCATION = 1,
    POLIGON = 2,
    ROUTE = 3
}