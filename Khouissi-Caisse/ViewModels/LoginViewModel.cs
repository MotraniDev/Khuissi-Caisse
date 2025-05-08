using Khouissi_Caisse.Models;
using Khouissi_Caisse.Services;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Khouissi_Caisse.ViewModels;

/// <summary>
/// ViewModel for the login page
/// </summary>
public class LoginViewModel : ViewModelBase
{
    private readonly IUserService _userService;
    private string _username = string.Empty;
    private string _password = string.Empty;
    private string _errorMessage = string.Empty;
    private bool _isLoading;
    private bool _rememberMe;

    /// <summary>
    /// Username for login
    /// </summary>
    public string Username
    {
        get => _username;
        set => SetProperty(ref _username, value);
    }

    /// <summary>
    /// Password for login
    /// </summary>
    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    /// <summary>
    /// Error message to display if login fails
    /// </summary>
    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }

    /// <summary>
    /// Indicates if authentication is in progress
    /// </summary>
    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    /// <summary>
    /// Indicates if user wants to be remembered
    /// </summary>
    public bool RememberMe
    {
        get => _rememberMe;
        set => SetProperty(ref _rememberMe, value);
    }

    /// <summary>
    /// Command to execute login process
    /// </summary>
    public ICommand LoginCommand { get; }

    /// <summary>
    /// Command to cancel login process
    /// </summary>
    public ICommand CancelCommand { get; }

    /// <summary>
    /// Event raised when login is successful
    /// </summary>
    public event EventHandler<User>? LoginSuccessful;

    /// <summary>
    /// Creates a new instance of LoginViewModel
    /// </summary>
    /// <param name="userService">User service for authentication</param>
    public LoginViewModel(IUserService userService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        LoginCommand = new RelayCommand(async _ => await LoginAsync(), _ => CanLogin());
        CancelCommand = new RelayCommand(_ => CancelLogin());
    }

    private bool CanLogin()
    {
        return !string.IsNullOrWhiteSpace(Username) &&
               !string.IsNullOrWhiteSpace(Password) &&
               !IsLoading;
    }

    private async Task LoginAsync()
    {
        ErrorMessage = string.Empty;
        IsLoading = true;

        try
        {
            var user = await _userService.AuthenticateAsync(Username, Password);

            if (user != null)
            {
                // Successful login
                LoginSuccessful?.Invoke(this, user);
            }
            else
            {
                // Failed login - Use resource key for InvalidCredentials
                ErrorMessage = Application.Current.Resources["InvalidCredentials"]?.ToString() ?? "Invalid username or password";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    private void CancelLogin()
    {
        Username = string.Empty;
        Password = string.Empty;
        ErrorMessage = string.Empty;
        IsLoading = false;
    }
}