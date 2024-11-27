using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Shapes;
using GMap.NET;
using GMap.NET.WindowsPresentation;

namespace FieldSimultation.Code.Markers;

public class PoligonMarker : IMarker
{
    private Guid _markerId;
    private Color _userPreferedColor;

    private List<PointLatLng> _poligonPoints;

    public PoligonMarker(Color userPreferedColor) {
        _markerId = Guid.NewGuid();
        _userPreferedColor = userPreferedColor;
        _poligonPoints = new List<PointLatLng>();
    }

    
    public void AddMarker(GMapControl control, PointLatLng point)
    {
        GMapMarker existing_poligon = control.Markers.FirstOrDefault(m=> m.Tag.ToString() == _markerId.ToString());
        if(existing_poligon != null) {
            control.Markers.Remove(existing_poligon);
        };

        _poligonPoints.Add(point);

        GMapPolygon poligon = new GMapPolygon(_poligonPoints) {
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
            
        control.Markers.Add(poligon);
    }
}
