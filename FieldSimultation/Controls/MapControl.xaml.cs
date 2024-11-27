using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using GMap.NET;
using GMap.NET.MapProviders;
using Code.OwnPosition;
using Code.Models;
using System.Reflection;
using System;

namespace FieldSimultation.Controls;

 public partial class MapControl: UserControl 
 {
    public event EventHandler MapClosed;
    public MapControl()
    {
        InitializeComponent();
    }

    public void InitializeMap(MissionDto mission)
    {
        // initMap
        map.MapProvider = GMapProviders.OpenStreetMap; // Use OpenStreetMap
        map.Position = new PointLatLng(mission.StartPositionLat, mission.StartPositionLng); // New York City
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

    private void OnMapClosed (object sender, RoutedEventArgs e)
    {
        MapClosed?.Invoke(sender, new EventArgs());
    }
 }