using System;
using System.Windows;
using System.Windows.Controls;
using FieldSimultation.Code.Services;
using FieldSimultation.Code.Models;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Input;

namespace FieldSimultation.Controls;

 public partial class GridView: UserControl 
 {

    public event EventHandler<MissionDto> ClickOpenMission; 
    public GridView() 
    {
        InitializeComponent();  
    }

    public void setDataSource(List<MissionDto> missions) 
    {
        PopulateGrid(missions);
    }

    private void PopulateGrid(List<MissionDto> items)
    {
        DynamicGrid.Children.Clear();
        DynamicGrid.RowDefinitions.Clear();
        DynamicGrid.ColumnDefinitions.Clear();

        // Add rows and columns
        for (int i = 0; i <= items.Count; i++)
        {
            DynamicGrid.RowDefinitions.Add(new RowDefinition {
                Height = new GridLength(40)
            });
        }

        for (int j = 0; j < 4; j++)
        {
            DynamicGrid.ColumnDefinitions.Add(new ColumnDefinition {
                 Width = new GridLength(1, GridUnitType.Star)
            });
        }

        // Populate the grid with items
        for (int i = 0; i <= items.Count; i++)
        {
            if(i==0) 
            {
                //set heder row
                SetGridTextColumn("Mission", i, 0, true);
                SetGridTextColumn("Start Position Lat", i, 1, true);
                SetGridTextColumn("Start Position Lng", i, 2, true);
                SetGridTextColumn("Open", i, 3, true);
            }
            else 
            {
                //add data row
                MissionDto mission = items[i-1];
                SetGridTextColumn(mission.FullName, i, 0);
                SetGridTextColumn(mission.StartPositionLat.ToString(), i, 1);
                SetGridTextColumn(mission.StartPositionLng.ToString(), i, 2);
                SetGridActionColumn("Open", i, 3, mission);
            }
        }
    }

    private void SetGridTextColumn(string value, int row, int column, bool headerRow = false) 
    {
        var cellContent = new Border
        {   
            BorderThickness = new Thickness(0.5),
            VerticalAlignment = VerticalAlignment.Stretch,
            HorizontalAlignment = HorizontalAlignment.Stretch,
        };
        cellContent.Child = new TextBlock
        {
            Text = value,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(5)
        };

        if(!headerRow)
        {
            cellContent.BorderBrush = Brushes.Gray;
            cellContent.Background = Brushes.LightGray;
        }
        else
        {
            cellContent.BorderBrush = Brushes.SlateGray;
            cellContent.Background = Brushes.LightSlateGray;
        }

        Grid.SetRow(cellContent, row);
        Grid.SetColumn(cellContent, column);
        DynamicGrid.Children.Add(cellContent);
    }

    private void SetGridActionColumn(string buttonText, int row, int column, MissionDto data) 
    {
        var cellContent = new Border
        {
            BorderBrush =  Brushes.Gray,     
            BorderThickness = new Thickness(0.5),
            VerticalAlignment = VerticalAlignment.Stretch,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Background = Brushes.LightGray
        };


        Button actionButton = new Button
        {
            Content = buttonText,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch,
            Margin = new Thickness(5),
            BorderBrush = Brushes.SlateGray,
            Background = Brushes.LightSlateGray,
            
        };
        actionButton.Click += (sender,e) => ClickOpenMission?.Invoke(this, data);
        cellContent.Child = actionButton;

        Grid.SetRow(cellContent, row);
        Grid.SetColumn(cellContent, column);
        DynamicGrid.Children.Add(cellContent);
    }
 }