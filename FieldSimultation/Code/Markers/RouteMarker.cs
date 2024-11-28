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

public class RouteMarker : IMarker
{
    private RouteMarkerData _data = new RouteMarkerData();
    public RouteMarker(string userPreferedColor) {
        _data.UserPreferedColor = userPreferedColor;
        _data.MarkerId = Guid.NewGuid();
        _data.RoutePoints = new List<PointLatLng>();
    }

    public string getData()
    {
       return JsonConvert.SerializeObject(_data);
    }

    public void AddMarker(GMapControl control, string data)
    {
        RouteMarkerData markerData = JsonConvert.DeserializeObject<RouteMarkerData>(data);
        AddPointToMarker(control, markerData);
    }

    public void AddPointToMarker(GMapControl control, PointLatLng point, StaffDto staff)
    {
        GMapMarker existing_route = control.Markers.FirstOrDefault(m => m.Tag.ToString() == _data.MarkerId.ToString());
        if (existing_route != null)
        {
            control.Markers.Remove(existing_route);
        };
        _data.Staff = staff;
        _data.RoutePoints.Add(point);
        AddPointToMarker(control, _data);
    }

    private void AddPointToMarker(GMapControl control, RouteMarkerData data)
    {
        SolidColorBrush brush = new((Color)ColorConverter.ConvertFromString(data.UserPreferedColor));
        GMapRoute route = new GMapRoute(data.RoutePoints)
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
        control.Markers.Add(route);
    }
}

public class RouteMarkerData {
    public Guid MarkerId { get; set; } 
    public string UserPreferedColor { get; set; }
    public List<PointLatLng> RoutePoints { get; set; }
    public StaffDto Staff { get; set; }
}