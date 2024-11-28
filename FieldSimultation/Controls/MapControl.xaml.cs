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
using FieldSimultation.Code.Services;

namespace FieldSimultation.Controls;

 public partial class MapControl: UserControl 
 {
    public event EventHandler MapClosed;
    private string _initials;
    private string _userColor;
    private StaffDto _staff;
    private MissionDto _mission;
    private MapShapeService _mapShapeSercice;
    private MissionService _missionServices;
    private MarkerRecorder _markerRecorder;


    private bool _isEditMode = false;
    public MapControl()
    {
        InitializeComponent();
        _markerRecorder = new MarkerRecorder();
        _mapShapeSercice = new MapShapeService();
        _missionServices = new MissionService();
    }

    public void InitializeMap(MissionDto mission)
    {
        _mission = mission;
        SetMapConfiguration(mission);
        LoadAllMapShapesFromDB(mission);
    }

    public void InitializeMarker(StaffDto staff) {
        _initials = staff.Initials;
        _staff = staff;
        _userColor = staff.IdentificationColor;
    }

    private void SetMapConfiguration(MissionDto mission)
    {
        map.MapProvider = GMapProviders.OpenStreetMap;
        map.Position = new PointLatLng(mission.StartPositionLat, mission.StartPositionLng);
        map.MinZoom = 1;
        map.MaxZoom = 18;
        map.Zoom = 10;
        map.DragButton = MouseButton.Left;
        map.ShowCenter = false;
    }

    private async void LoadAllMapShapesFromDB(MissionDto mission)
    {
        List<MapShapeDto> mapShapes = await _missionServices.getAllMapShapesByMissionId(mission.Id);
        foreach(MapShapeDto mapShape in mapShapes) {
            _markerRecorder.InitializeMarker(mapShape.Type, _initials, _userColor);
            _markerRecorder.AddMarker(map, mapShape);
        }
    }
    
    private void MapControl_LeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if(_isEditMode == true) {
            var clickPosition = e.GetPosition(map);
            PointLatLng point = map.FromLocalToLatLng((int)clickPosition.X, (int)clickPosition.Y);
            _markerRecorder.AddPointToMarker(map, point, _staff);
        }
    }

    private void Drow_Click(object sender, RoutedEventArgs e)
    {
        MarkerType? markerType = getMarkerType();
        if(markerType != null) {
            SetEditMode(true);
            _markerRecorder.InitializeMarker(markerType.Value, _initials, _userColor);
        }
    }
    private void Save_Click(object sender, RoutedEventArgs e) {
       if(saveMarkersToDatabase()) {
            MapShapeDto dto = _markerRecorder.GetMapShapeToData();
            dto.MissionId = _mission.Id;
            _mapShapeSercice.SaveMapShapeService(dto);
        }
        SetEditMode(false);
    }

    private void OnMapClosed (object sender, RoutedEventArgs e)
    {
        map.Markers.Clear();
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

    private bool saveMarkersToDatabase() {
        var radionChk = SaveOperation.Children.OfType<RadioButton>().FirstOrDefault(m => m.IsChecked == true);
        if(radionChk != null) {
            return string.Equals(radionChk.Tag.ToString(), "yes");
        }
        return false;
    }
 }