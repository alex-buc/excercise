using System.Linq;
using System.Windows.Media;
using System.Windows.Shapes;
using FieldSimultation.Code.Models;
using FieldSimultation.Code.Shared;

using GMap.NET;
using GMap.NET.WindowsPresentation;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;

namespace FieldSimultation.Code.Markers;

public class OwnPositionMarker: IMarker
{
    private OwnPositionMarkerData _data = new OwnPositionMarkerData();

    public OwnPositionMarker(string initials, string userPreferedColor)
    {
        _data.MarkerId = $"{initials}-own-position";
        _data.Initials = initials;
        _data.UserPreferedColor = userPreferedColor;
    }

    public string getData()
    {
       return JsonConvert.SerializeObject(_data);
    }
    
    public void AddMarker(GMapControl control, string data)
    {
        OwnPositionMarkerData markerData = JsonConvert.DeserializeObject<OwnPositionMarkerData>(data);
        AddPointToMarker(control, markerData);
    }

    public void AddPointToMarker(GMapControl control, PointLatLng point, StaffDto staff)
    {
        GMapMarker existingMarker = control.Markers.FirstOrDefault(m => m.Tag.ToString() == _data.MarkerId);
        if (existingMarker != null)
        {
            control.Markers.Remove(existingMarker);
        };
        _data.Point = point;
        _data.Staff = staff;
        AddPointToMarker(control, _data);
    }

    private void AddPointToMarker(GMapControl control, OwnPositionMarkerData data)
    {
        Color color = (Color)ColorConverter.ConvertFromString(data.UserPreferedColor);
        SolidColorBrush brush = new(color);
        GMapMarker marker = new GMapMarker(data.Point)
        {
            Shape = new Ellipse
            {
                Width = 40,
                Height = 40,
                Fill = new ImageBrush
                {
                    ImageSource = BitmapImageHelper.CreateInitialsImage(data.Initials, color, Colors.White)
                },
                Stroke = brush,
                StrokeThickness = 1
            },
            Tag = data.MarkerId
        };
        control.Markers.Add(marker);
    }
}

public class OwnPositionMarkerData {
    public string MarkerId { get; set; }
    public string Initials { get; set; }
    public string UserPreferedColor { get; set; }
    public PointLatLng Point { get; set; }
    public StaffDto Staff { get; set; }
}