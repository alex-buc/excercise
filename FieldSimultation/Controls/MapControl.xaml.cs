using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using GMap.NET;
using GMap.NET.MapProviders;
using FieldSimultation.Code.OwnPosition;
using FieldSimultation.Code.Models;
using System;
using System.Windows.Media;

namespace FieldSimultation.Controls;

 public partial class MapControl: UserControl 
 {
    public event EventHandler MapClosed;
    private string _initials;
    private Color _userColor;
    public MapControl()
    {
        InitializeComponent();
    }

    public void InitializeMap(MissionDto mission)
    {
        map.MapProvider = GMapProviders.OpenStreetMap;
        map.Position = new PointLatLng(mission.StartPositionLat, mission.StartPositionLng);
        map.MinZoom = 1;
        map.MaxZoom = 18;
        map.Zoom = 10;
        map.DragButton = MouseButton.Left;
        map.ShowCenter = false;
    }

    public void InitializeMarker(string initials, string hexColor) {
        _initials = initials;
        _userColor = (Color)ColorConverter.ConvertFromString(hexColor);
    }

    private void MapControl_RightButtonDown(object sender, MouseButtonEventArgs e)
    {
        var clickPosition = e.GetPosition(map);
        PointLatLng point = map.FromLocalToLatLng((int)clickPosition.X, (int)clickPosition.Y);
        map.Markers.Add(OwnPositionHelper.AddMarker(point, _initials, _userColor));
    }

    private void OnMapClosed (object sender, RoutedEventArgs e)
    {
        MapClosed?.Invoke(sender, new EventArgs());
    }
 }