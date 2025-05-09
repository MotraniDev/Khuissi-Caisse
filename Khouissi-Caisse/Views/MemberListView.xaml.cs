using Khouissi_Caisse.Models;
using Khouissi_Caisse.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Khouissi_Caisse.Views
{
    /// <summary>
    /// Interaction logic for MemberListView.xaml
    /// </summary>
    public partial class MemberListView : Page
    {
        private MemberListViewModel _viewModel => (MemberListViewModel)DataContext;

        /// <summary>
        /// Initializes a new instance of the MemberListView
        /// </summary>
        public MemberListView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles double-click on a member row
        /// </summary>
        private void MembersList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_viewModel != null && _viewModel.ViewMemberCommand.CanExecute(null))
            {
                _viewModel.ViewMemberCommand.Execute(null);
            }
        }

        /// <summary>
        /// Handles Enter key press on a member row
        /// </summary>
        private void MembersList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && _viewModel != null && _viewModel.ViewMemberCommand.CanExecute(null))
            {
                _viewModel.ViewMemberCommand.Execute(null);
            }
        }
    }
}
