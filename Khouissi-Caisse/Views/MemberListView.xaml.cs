using Khouissi_Caisse.Models;
using Khouissi_Caisse.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Khouissi_Caisse.Views
{
    /// <summary>
    /// Interaction logic for MemberListView.xaml
    /// </summary>
    public partial class MemberListView : UserControl
    {
        private MemberListViewModel? _viewModel;

        /// <summary>
        /// Initializes a new instance of the MemberListView
        /// </summary>
        public MemberListView()
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                _viewModel = DataContext as MemberListViewModel;
            };
        }        /// <summary>
                 /// Handles double-click on a member row
                 /// </summary>
        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_viewModel != null && _viewModel.SelectedMember != null)
            {
                if (_viewModel.ViewMemberCommand.CanExecute(null))
                {
                    _viewModel.ViewMemberCommand.Execute(null);
                }
            }
        }
    }
}
