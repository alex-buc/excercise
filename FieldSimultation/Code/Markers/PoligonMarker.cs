using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Shapes;
using FieldSimultation.Code.Models;
using GMap.NET;
using GMap.NET.WindowsPresentation;
using Newtonsoft.Json;

namespace FieldSimultation.Code.Markers;

public class PoligonMarker : IMarker
{
    private PoligonMarkerData _data = new PoligonMarkerData();

    public PoligonMarker(string userPreferedColor) {
        _data.MarkerId = Guid.NewGuid();
        _data.UserPreferedColor = userPreferedColor;
        _data.PoligonPoints = new List<PointLatLng>();
    }

    public string getData()
    {
       return JsonConvert.SerializeObject(_data);
    }

    public void AddMarker(GMapControl control, string data)
    {
        PoligonMarkerData markerData = JsonConvert.DeserializeObject<PoligonMarkerData>(data);
        AddPointToMarker(control, markerData);
    }
    
    public void AddPointToMarker(GMapControl control, PointLatLng point, StaffDto staff)
    {
        GMapMarker existing_poligon = control.Markers.FirstOrDefault(m => m.Tag.ToString() == _data.MarkerId.ToString());
        if (existing_poligon != null)
        {
            control.Markers.Remove(existing_poligon);
        };

        _data.PoligonPoints.Add(point);
        _data.Staff = staff;

        AddPointToMarker(control, _data);
    }

    private void AddPointToMarker(GMapControl control, PoligonMarkerData data)
    {
        SolidColorBrush brush = new((Color)ColorConverter.ConvertFromString(data.UserPreferedColor));

        GMapPolygon poligon = new GMapPolygon(data.PoligonPoints)
        {
            Shape = new Ellipse
            {
                Width = 40,
                Height = 40,
                Fill = brush,
                Stroke = brush,
                StrokeThickness = 1
            },
            Tag = data.MarkerId
        };

        control.Markers.Add(poligon);
    }
}

public class PoligonMarkerData {
    public Guid MarkerId { get; set; } 
    public string UserPreferedColor { get; set; }
    public List<PointLatLng> PoligonPoints { get; set; }
    public StaffDto Staff { get; set; }
}
