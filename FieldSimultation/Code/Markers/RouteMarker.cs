using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Shapes;
using GMap.NET;
using GMap.NET.WindowsPresentation;

namespace FieldSimultation.Code.Markers;

public class RouteMarker : IMarker
{
    private Guid _markerId;
    private Color _userPreferedColor;

    private List<PointLatLng> _routePoints;

    public RouteMarker(Color userPreferedColor) {
        _userPreferedColor = userPreferedColor;
        _markerId = Guid.NewGuid();
        _routePoints = new List<PointLatLng>();
    }

    public void AddMarker(GMapControl control, PointLatLng point)
    {
         GMapMarker existing_route = control.Markers.FirstOrDefault(m=> m.Tag.ToString() == _markerId.ToString());
        if(existing_route != null) {
            control.Markers.Remove(existing_route);
        };

        _routePoints.Add(point);

        GMapRoute route = new GMapRoute(_routePoints) {
            Shape = new Ellipse
            {
                Width = 40,
                Height = 40,
                Fill =  new SolidColorBrush(_userPreferedColor),
                Stroke = new SolidColorBrush(_userPreferedColor),
                StrokeThickness = 1
            },
            Tag = _markerId
        };
            
        control.Markers.Add(route);
    }
}