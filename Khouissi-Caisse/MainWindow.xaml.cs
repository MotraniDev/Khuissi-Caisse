using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
    private readonly IMemberService _memberService;
    private User? _currentUser;
    private Button? _activeNavButton;

    /// <summary>
    /// Creates an instance of the main window
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();

        // Get services from DI container
        _userService = App.ServiceProvider.GetRequiredService<IUserService>();
        _memberService = App.ServiceProvider.GetRequiredService<IMemberService>();

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

    #region Navigation Methods

    /// <summary>
    /// Home navigation button click handler
    /// </summary>
    private void HomeButton_Click(object sender, RoutedEventArgs e)
    {
        if (_currentUser == null)
        {
            ShowLoginView();
            return;
        }

        SetActiveNavButton(HomeButton);
        // For now, just clear the content
        MainContent.Content = null;
    }

    /// <summary>
    /// Members navigation button click handler
    /// </summary>
    private void MembersButton_Click(object sender, RoutedEventArgs e)
    {
        if (_currentUser == null)
        {
            ShowLoginView();
            return;
        }

        SetActiveNavButton(MembersButton);
        ShowMembersListView();
    }

    /// <summary>
    /// Subscriptions navigation button click handler
    /// </summary>
    private void SubscriptionsButton_Click(object sender, RoutedEventArgs e)
    {
        if (_currentUser == null)
        {
            ShowLoginView();
            return;
        }

        SetActiveNavButton(SubscriptionsButton);
        // TODO: Show subscriptions view when implemented
        MessageBox.Show("ستتم إضافة وظيفة الاشتراكات في المرحلة التالية", "قيد التطوير", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    /// <summary>
    /// Expenses navigation button click handler
    /// </summary>
    private void ExpensesButton_Click(object sender, RoutedEventArgs e)
    {
        if (_currentUser == null)
        {
            ShowLoginView();
            return;
        }

        SetActiveNavButton(ExpensesButton);
        // TODO: Show expenses view when implemented
        MessageBox.Show("ستتم إضافة وظيفة المصروفات في المرحلة التالية", "قيد التطوير", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    /// <summary>
    /// Reports navigation button click handler
    /// </summary>
    private void ReportsButton_Click(object sender, RoutedEventArgs e)
    {
        if (_currentUser == null)
        {
            ShowLoginView();
            return;
        }

        SetActiveNavButton(ReportsButton);
        // TODO: Show reports view when implemented
        MessageBox.Show("ستتم إضافة وظيفة التقارير في المرحلة التالية", "قيد التطوير", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    /// <summary>
    /// Settings navigation button click handler
    /// </summary>
    private void SettingsButton_Click(object sender, RoutedEventArgs e)
    {
        if (_currentUser == null)
        {
            ShowLoginView();
            return;
        }

        SetActiveNavButton(SettingsButton);
        // TODO: Show settings view when implemented
        MessageBox.Show("ستتم إضافة وظيفة الإعدادات في المرحلة التالية", "قيد التطوير", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    #endregion

    /// <summary>
    /// Sets the active navigation button style
    /// </summary>
    private void SetActiveNavButton(Button button)
    {
        // Reset previous active button style
        if (_activeNavButton != null)
        {
            _activeNavButton.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#2c3e50");
        }

        // Set new active button style
        _activeNavButton = button;
        button.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#3498db");
    }

    /// <summary>
    /// Shows the members list view
    /// </summary>
    private void ShowMembersListView()
    {
        var memberListViewModel = App.ServiceProvider.GetRequiredService<MemberListViewModel>();

        // Handle view member details request
        memberListViewModel.ViewMemberRequested += (sender, member) =>
        {
            // TODO: Show member details view when implemented
            if (member != null)
            {
                MessageBox.Show($"تم طلب عرض تفاصيل العضو: {member.FullName}", "عرض التفاصيل", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        };

        // Handle edit member request
        memberListViewModel.EditMemberRequested += (sender, member) =>
        {
            // TODO: Show member edit view when implemented
            if (member != null)
            {
                MessageBox.Show($"تم طلب تعديل العضو: {member.FullName}", "تعديل", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        };

        // Handle add member request
        memberListViewModel.AddMemberRequested += (sender, args) =>
        {
            // TODO: Show add member view when implemented
            MessageBox.Show("تم طلب إضافة عضو جديد", "إضافة عضو", MessageBoxButton.OK, MessageBoxImage.Information);
        };

        var memberListView = new MemberListView
        {
            DataContext = memberListViewModel
        };

        MainContent.Content = memberListView;
    }
}
