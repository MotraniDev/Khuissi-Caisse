using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Khouissi_Caisse.Models;
using Khouissi_Caisse.Services;
using Khouissi_Caisse.ViewModels;
using Khouissi_Caisse.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Khouissi_Caisse;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly IUserService _userService;
    private User? _currentUser;

    /// <summary>
    /// Creates an instance of the main window
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();

        // Get service from DI container
        _userService = App.ServiceProvider.GetRequiredService<IUserService>();

        // Set up initial state
        UpdateLoginState();
    }

    /// <summary>
    /// Event handler for Login button click
    /// </summary>
    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        if (_currentUser == null)
        {
            // User is not logged in, show login view
            ShowLoginView();
        }
        else
        {
            // User is logged in, log out
            _currentUser = null;
            UpdateLoginState();
            MainContent.Content = null;
            MessageBox.Show("تم تسجيل الخروج بنجاح", "تسجيل الخروج", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    /// <summary>
    /// Shows the login view in the main content area
    /// </summary>
    private void ShowLoginView()
    {
        var loginViewModel = App.ServiceProvider.GetRequiredService<LoginViewModel>();
        loginViewModel.LoginSuccessful += (sender, user) =>
        {
            // Handle successful login
            _currentUser = user;
            UpdateLoginState();
            MainContent.Content = null; // Clear login view
            MessageBox.Show($"مرحبًا {user.FirstName}!", "تسجيل الدخول", MessageBoxButton.OK, MessageBoxImage.Information);
        };

        var loginView = new LoginView
        {
            DataContext = loginViewModel
        };

        MainContent.Content = loginView;
    }

    /// <summary>
    /// Updates UI elements based on login state
    /// </summary>
    private void UpdateLoginState()
    {
        if (_currentUser != null)
        {
            UserNameText.Text = $"{_currentUser.FirstName} {_currentUser.LastName}";
            LoginButton.Content = "تسجيل الخروج";
        }
        else
        {
            UserNameText.Text = "مستخدم غير مسجل";
            LoginButton.SetResourceReference(Button.ContentProperty, "Login");
        }
    }
}