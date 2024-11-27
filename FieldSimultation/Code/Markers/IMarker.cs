using GMap.NET;
using GMap.NET.WindowsPresentation;

namespace FieldSimultation.Code.Markers;

public interface IMarker {
    void AddMarker(GMapControl control, PointLatLng point);
}