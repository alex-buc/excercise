using System;
using System.Windows;
using System.Windows.Controls;
using FieldSimultation.Code.Services;
using FieldSimultation.Code.Models;

namespace FieldSimultation.Controls;

 public partial class UserLogin: UserControl 
 {
    public event EventHandler<int> LoginSuccess;
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
            LoginSuccess?.Invoke(this, user.Id.Value);
        }
        catch {
            MessageBox.Show("Invalid credentials!");
        }
    }
}