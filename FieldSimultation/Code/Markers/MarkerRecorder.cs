using System.Windows.Media;
using FieldSimultation.Code.Models;
using GMap.NET;
using GMap.NET.WindowsPresentation;

namespace FieldSimultation.Code.Markers; 

public class MarkerRecorder
{
    protected IMarker? _marker;
    protected MarkerType _markerType;
    protected int? _id;
    public MarkerRecorder() {

    }
    public void InitializeMarker(MarkerType markerType, string initials, string userPreferedColor) {
        _markerType = markerType;
        switch (markerType)
        {
            case MarkerType.OWN_LOCATION: {
                _marker = new OwnPositionMarker(initials, userPreferedColor);
                break;
            }
            case MarkerType.POLIGON: {
                _marker = new PoligonMarker(userPreferedColor);
                break;
            }
            case MarkerType.ROUTE: {
                _marker = new RouteMarker(userPreferedColor);
                break;
            }
            default: {
                break;
            }
        }
    }

    public void AddPointToMarker (GMapControl control, PointLatLng point, StaffDto staff) {
        _marker?.AddPointToMarker(control, point, staff);
    }

    public void AddMarker(GMapControl control, MapShapeDto data) {
        _id = data.Id;
        _marker?.AddMarker(control, data.Data);
    }

    internal MapShapeDto GetMapShapeToData()
    {
        string data =_marker?.getData();
        return new MapShapeDto() {
            Id = _id,
            Data = data,
            Type = _markerType
        };
    }
}