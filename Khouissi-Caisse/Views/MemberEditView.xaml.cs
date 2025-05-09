using Khouissi_Caisse.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Khouissi_Caisse.Views
{
    /// <summary>
    /// Interaction logic for MemberEditView.xaml
    /// </summary>
    public partial class MemberEditView : Page
    {
        /// <summary>
        /// Initialize the MemberEditView control
        /// </summary>
        public MemberEditView()
        {
            InitializeComponent();
            
            // Handle view loading
            this.Loaded += MemberEditView_Loaded;
            
            // Handle navigation
            this.Unloaded += MemberEditView_Unloaded;
        }

        /// <summary>
        /// Handle the view loaded event
        /// </summary>
        private void MemberEditView_Loaded(object sender, RoutedEventArgs e)
        {
            // Get the ViewModel
            if (DataContext is MemberEditViewModel viewModel)
            {
                // Set initial focus to the first name field if it exists
                if (FindName("FirstNameTextBox") is TextBox firstNameTextBox)
                {
                    firstNameTextBox.Focus();
                }
            }
        }

        /// <summary>
        /// Handle when the page is unloaded (equivalent to navigating away)
        /// </summary>
        private void MemberEditView_Unloaded(object sender, RoutedEventArgs e)
        {
            // Clean up any resources if needed
            if (DataContext is MemberEditViewModel viewModel)
            {
                // Perform any cleanup needed when navigating away
            }
        }
    }
}
