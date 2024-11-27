using System.Linq;
using System.Windows.Media;
using System.Windows.Shapes;
using FieldSimultation.Code.Shared;

using GMap.NET;
using GMap.NET.WindowsPresentation;

namespace FieldSimultation.Code.Markers;

public class OwnPositionMarker: IMarker
{
    private string _markerId;
    private string _initials;
    private Color _userPreferedColor;

    public OwnPositionMarker(string initials, Color userPreferedColor)
    {
        _markerId = $"{initials}-own-position";
        _initials = initials;
        _userPreferedColor = userPreferedColor;
    }

    public void AddMarker(GMapControl control, PointLatLng point)
    {
        GMapMarker existing_poligon = control.Markers.FirstOrDefault(m => m.Tag.ToString() == _markerId.ToString());
        if(existing_poligon != null) {
            control.Markers.Remove(existing_poligon);
        };

        // Create a new marker
        GMapMarker marker = new GMapMarker(point)
        {
            Shape = new Ellipse
            {
                Width = 40,
                Height = 40,
                Fill =  new ImageBrush {
                    ImageSource = BitmapImageHelper.CreateInitialsImage(_initials, _userPreferedColor, Colors.White)
                },
                Stroke = new SolidColorBrush(_userPreferedColor),
                StrokeThickness = 1
            },
            Tag = _markerId
        };
        control.Markers.Add(marker);
    }
}