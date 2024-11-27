using System;
using System.Windows;
using FieldSimultation.Controls;
using FieldSimultation.Code.Services;
using FieldSimultation.Code.Models;

namespace FieldSimultation;

/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        
        var loginControl = new UserLogin();
        loginControl.LoginSuccess += OnLoginSuccess; 
        MainContentControl.Content = loginControl; 
    }

    private async void OnLoginSuccess(object sender, int userId)
    {
        try {
            UserService userService = new UserService();
            StaffDto staff = await userService.GetStaffInfoAsync(userId);
            var mainContent = new MainContainer(staff);
            MainContentControl.Content = mainContent;
        }
        catch(Exception e) {
             MessageBox.Show("Could not retrive staff info!");
        }
    }
}