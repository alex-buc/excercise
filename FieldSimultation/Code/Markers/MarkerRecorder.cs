using System.Windows.Media;
using GMap.NET;
using GMap.NET.WindowsPresentation;

namespace FieldSimultation.Code.Markers; 

public class MarkerRecorder
{
    protected IMarker? _grupMarker;

    public MarkerRecorder(MarkerType markerType, string initials, Color userPreferedColor) {
        switch (markerType)
        {
            case MarkerType.OWN_LOCATION: {
                _grupMarker = new OwnPositionMarker(initials, userPreferedColor);
                break;
            }
            case MarkerType.SYMBOL: {
                break;
            }
            case MarkerType.POLIGON: {
                _grupMarker = new PoligonMarker(userPreferedColor);
                break;
            }
            case MarkerType.ROUTE: {
                break;
            }
            default: {
                break;
            }
        }
    }
    public void AddPointToMarker (GMapControl control, PointLatLng point) {
        _grupMarker?.AddMarker(control, point);
    }
}

public enum MarkerType 
{
    OWN_LOCATION = 1,
    SYMBOL = 2,
    POLIGON = 3,
    ROUTE = 4
}