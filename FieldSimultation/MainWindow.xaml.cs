using System;
using System.Windows;
using FieldSimultation.Controls;

namespace FieldSimultation;

/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        
        var loginControl = new UserLogin();
        loginControl.LoginSuccess += OnLoginSuccess; // Subscribe to the login success event
        MainContentControl.Content = loginControl;  // Set the login control as the current content
    }

    private void OnLoginSuccess(object sender, EventArgs e)
    {
        // Switch to the main user control upon successful login
        var mainContent = new MapControl();
        MainContentControl.Content = mainContent;
    }
}