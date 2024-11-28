using FieldSimultation.Code.Models;
using GMap.NET;
using GMap.NET.WindowsPresentation;

namespace FieldSimultation.Code.Markers;

public interface IMarker {
    void AddPointToMarker(GMapControl control, PointLatLng point, StaffDto staff);
    void AddMarker(GMapControl control, string data);
    string getData();
}