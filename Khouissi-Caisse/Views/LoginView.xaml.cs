using Khouissi_Caisse.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Khouissi_Caisse.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        private LoginViewModel? _viewModel; // Removed readonly modifier

        /// <summary>
        /// Initialize the LoginView control
        /// </summary>
        public LoginView()
        {
            InitializeComponent();

            // Get the DataContext as LoginViewModel
            Loaded += (s, e) =>
            {
                _viewModel = DataContext as LoginViewModel;
                if (_viewModel != null)
                {
                    // Handle PasswordBox (since it can't be bound directly due to security)
                    PasswordBox.Password = _viewModel.Password;
                    PasswordBox.PasswordChanged += PasswordBox_PasswordChanged;
                }
            };
        }

        /// <summary>
        /// Updates the view model password when the PasswordBox content changes
        /// </summary>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.Password = PasswordBox.Password;
            }
        }
    }
}