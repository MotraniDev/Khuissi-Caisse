using Khouissi_Caisse.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Khouissi_Caisse.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Page
    {
        private LoginViewModel? _viewModel;

        /// <summary>
        /// Initialize the LoginView control
        /// </summary>
        public LoginView()
        {
            InitializeComponent();
            this.Loaded += LoginView_Loaded;
        }

        private void LoginView_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel = DataContext as LoginViewModel;
            if (_viewModel != null)
            {
                // Handle PasswordBox (since it can't be bound directly due to security)
                PasswordBox.Password = _viewModel.Password;
                PasswordBox.PasswordChanged += PasswordBox_PasswordChanged;
                
                // Set default placeholder value as hint for the mock login
                PasswordBox.Tag = "Default password is 123";
                
                // Give focus to the password box immediately
                PasswordBox.Focus();
            }
        }

        /// <summary>
        /// Updates the view model password when the PasswordBox content changes
        /// </summary>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.Password = PasswordBox.Password;
                
                // Force update of CanExecute status for commands
                CommandManager.InvalidateRequerySuggested();
            }
        }
        
        /// <summary>
        /// Handle key press in password box to support Enter key for login
        /// </summary>
        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && _viewModel != null)
            {
                if (_viewModel.LoginCommand.CanExecute(null))
                {
                    _viewModel.LoginCommand.Execute(null);
                    e.Handled = true;
                }
            }
        }
        
        /// <summary>
        /// Handle click event for the login button as a backup to command binding
        /// </summary>
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null && _viewModel.LoginCommand.CanExecute(null))
            {
                _viewModel.LoginCommand.Execute(null);
            }
        }
    }
}
