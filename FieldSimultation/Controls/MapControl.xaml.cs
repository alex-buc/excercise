using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using GMap.NET;
using GMap.NET.MapProviders;
using FieldSimultation.Code.Markers;
using FieldSimultation.Code.Models;
using System;
using System.Linq;
using System.Windows.Media;
using GMap.NET.WindowsPresentation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Windows.Markup.Primitives;

namespace FieldSimultation.Controls;

 public partial class MapControl: UserControl 
 {
    public event EventHandler MapClosed;
    private string _initials;
    private Color _userColor;

    private MarkerRecorder _markerRecorder;

    private bool _isEditMode = false;
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

    private void MapControl_LeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if(_isEditMode == true) {
            var clickPosition = e.GetPosition(map);
            PointLatLng point = map.FromLocalToLatLng((int)clickPosition.X, (int)clickPosition.Y);
            _markerRecorder.AddPointToMarker(map, point);
        }
        
    }

    private void Drow_Click(object sender, RoutedEventArgs e)
    {
        MarkerType? markerType = getMarkerType();
        if(markerType != null) {
            SetEditMode(true);
            _markerRecorder = new MarkerRecorder(markerType.Value, _initials, _userColor);
        }
    }
    private void Save_Click(object sender, RoutedEventArgs e) {
       SetEditMode(false);
       _markerRecorder = null;
    }

    private void OnMapClosed (object sender, RoutedEventArgs e)
    {
        MapClosed?.Invoke(sender, new EventArgs());
    }

    private void SetEditMode(bool isEditing)
    {
        map.CanDragMap = !isEditing;
        _isEditMode = isEditing;
        DrowOperations.Visibility = isEditing?Visibility.Collapsed: Visibility.Visible;
        SaveOperation.Visibility = isEditing?Visibility.Visible:Visibility.Collapsed;
    }

    private MarkerType? getMarkerType() {
        var radionChk = DrowOperations.Children.OfType<RadioButton>().FirstOrDefault(m => m.IsChecked == true);
        if(radionChk != null) {
            return (MarkerType)Enum.Parse(typeof(MarkerType), radionChk.Tag.ToString());
        }
        return null;
    }
 }