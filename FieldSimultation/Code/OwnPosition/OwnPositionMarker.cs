using System.Windows.Media;
using System.Windows.Shapes;
using FieldSimultation.Code.Shared;
using GMap.NET;
using GMap.NET.WindowsPresentation;

namespace FieldSimultation.Code.OwnPosition;

public static class OwnPositionHelper
{
    public static GMapMarker AddMarker(PointLatLng position, string initials, Color userPreferedColor)
    {
        // Create a new marker
        GMapMarker marker = new GMapMarker(position)
        {
            Shape = new Ellipse
            {
                Width = 40,
                Height = 40,
                Fill =  new ImageBrush {
                    ImageSource = BitmapImageHelper.CreateInitialsImage(initials, userPreferedColor, Colors.White)
                },
                Stroke = new SolidColorBrush(userPreferedColor),
                StrokeThickness = 1,  
                ToolTip = $"lat: {position.Lat}, Lng: {position.Lng}"
            }
        };
        return marker;
    }
}