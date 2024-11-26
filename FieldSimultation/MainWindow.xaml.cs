using System.Windows;
using System.Windows.Input;
using GMap.NET;
using GMap.NET.MapProviders;
using Code.OwnPosition;

namespace FieldSimultation;

/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        //initMap
        MapControl.MapProvider = GMapProviders.OpenStreetMap; // Use OpenStreetMap
        MapControl.Position = new PointLatLng(40.7128, -74.0060); // New York City
        MapControl.MinZoom = 1;
        MapControl.MaxZoom = 18;
        MapControl.Zoom = 10;
        MapControl.DragButton = MouseButton.Left;
        MapControl.ShowCenter = false;
    }

    private void MapControl_RightButtonDown(object sender, MouseButtonEventArgs e)
    {
        var clickPosition = e.GetPosition(MapControl);
        PointLatLng point = MapControl.FromLocalToLatLng((int)clickPosition.X, (int)clickPosition.Y);
        MapControl.Markers.Add(OwnPositionHelper.AddMarker(point));
    }
}