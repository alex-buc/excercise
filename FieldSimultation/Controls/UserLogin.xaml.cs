using System;
using System.Windows;
using System.Windows.Controls;

namespace FieldSimultation.Controls;

 public partial class UserLogin: UserControl 
 {
    public event EventHandler LoginSuccess;
    public UserLogin()
    {
        InitializeComponent();
    }

    private void OnLoginButtonClick(object sender, RoutedEventArgs e)
    {
        string username = UsernameTextBox.Text;
        string password = PasswordBox.Password;

        // Simple check for username and password
        if (username == "user" && password == "password")
        {
            // Trigger the login success event
            LoginSuccess?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            MessageBox.Show("Invalid credentials!");
        }
    }
}