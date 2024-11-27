using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using GMap.NET;
using GMap.NET.MapProviders;
using Code.OwnPosition;

namespace FieldSimultation.Controls;

 public partial class MapControl: UserControl 
 {
    public MapControl()
    {
        InitializeComponent();

        // initMap
        map.MapProvider = GMapProviders.OpenStreetMap; // Use OpenStreetMap
        map.Position = new PointLatLng(40.7128, -74.0060); // New York City
        map.MinZoom = 1;
        map.MaxZoom = 18;
        map.Zoom = 10;
        map.DragButton = MouseButton.Left;
        map.ShowCenter = false;
    }

    private void MapControl_RightButtonDown(object sender, MouseButtonEventArgs e)
    {
        var clickPosition = e.GetPosition(map);
        PointLatLng point = map.FromLocalToLatLng((int)clickPosition.X, (int)clickPosition.Y);
        map.Markers.Add(OwnPositionHelper.AddMarker(point));
    }
 }