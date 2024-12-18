using System;
using System.Windows;
using System.Windows.Controls;
using FieldSimultation.Code.Services;
using FieldSimultation.Code.Models;
using System.Collections.Generic;
using System.Windows.Media;
using FieldSimultation.Code.Shared;

namespace FieldSimultation.Controls;

public partial class MainContainer: UserControl 
{
    public MainContainer(StaffDto staff)
    {
        InitializeComponent();
        
        SetStaffInfo(staff);
        SetMissionGrid(staff);
        SetMapInfo(staff);

        Map.MapClosed += Map_Closed;
    }

    private void SetStaffInfo(StaffDto staff) {
        Message.Content = $"Welcome: {staff.FullName}";
        Color color = (Color)ColorConverter.ConvertFromString(staff.IdentificationColor);
        Initials.Source = BitmapImageHelper.CreateInitialsImage(staff.Initials, Colors.White, color);
    }

    private async void SetMissionGrid(StaffDto staff) {
        StaffService staffService = new StaffService();
        List<MissionDto> missions = await staffService.getAllMissionsByStuffId(staff.Id);
        MissionGrid.setDataSource(missions);
    }

    private void SetMapInfo(StaffDto staff) {
        Map.InitializeMarker(staff);
    }

    private void MissionGrid_ClickOpenMission(object sender, MissionDto mission) {
        MissionGrid.Visibility = Visibility.Collapsed;
        Map.Visibility = Visibility.Visible;
        Map.InitializeMap(mission);
    }

    private void Map_Closed(object sender, EventArgs e) 
    {
        MissionGrid.Visibility = Visibility.Visible;
        Map.Visibility = Visibility.Collapsed;
    }
}
