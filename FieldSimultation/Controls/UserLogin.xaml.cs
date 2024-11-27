using System;
using System.Windows;
using System.Windows.Controls;
using Code.Services;
using Code.Models;

namespace FieldSimultation.Controls;

 public partial class UserLogin: UserControl 
 {
    public event EventHandler LoginSuccess;
    public UserLogin()
    {
        InitializeComponent();
    }

    private async void OnLoginButtonClick(object sender, RoutedEventArgs e)
    {
        try
        {
            UserService userService = new UserService();
            UserDto user = await userService.GetUserAsync(UsernameTextBox.Text, PasswordBox.Password);
            LoginSuccess?.Invoke(this, EventArgs.Empty);
        }
        catch {
            MessageBox.Show("Invalid credentials!");
        }
    }
}