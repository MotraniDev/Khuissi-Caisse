using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Khouissi_Caisse.Models;
using Khouissi_Caisse.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks; // Added for Task
using System.Windows; // Added for MessageBox

namespace Khouissi_Caisse.ViewModels
{
    public partial class MemberListViewModel : ObservableObject
    {
        private readonly IMemberService _memberService;
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private ObservableCollection<Member> _members = new ObservableCollection<Member>();

        [ObservableProperty]
        private Member? _selectedMember;

        public MemberListViewModel(IMemberService memberService, INavigationService navigationService)
        {
            _memberService = memberService;
            _navigationService = navigationService;
            LoadMembers();
        }

        [RelayCommand]
        private void ViewMember()
        {
            if (SelectedMember != null)
            {
                _navigationService.NavigateTo(nameof(MemberDetailsViewModel), SelectedMember.Id);
            }
        }

        [RelayCommand]
        private void EditMember()
        {
            if (SelectedMember != null)
            {
                _navigationService.NavigateTo(nameof(MemberEditViewModel), SelectedMember.Id);
            }
        }

        [RelayCommand]
        private void AddMember()
        {
            _navigationService.NavigateTo(nameof(MemberEditViewModel));
        }

        private async void LoadMembers()
        {
            var members = await _memberService.GetAllAsync();
            Members = new ObservableCollection<Member>(members);
        }

        public async Task LoadMembersAsync()
        {
            var memberList = await _memberService.GetAllAsync();
            Members.Clear();
            foreach (var member in memberList)
            {
                Members.Add(member);
            }
        }

        [RelayCommand]
        private void NavigateToMemberDetails()
        {
            if (SelectedMember != null)
            {
                _navigationService.NavigateTo(nameof(MemberDetailsViewModel), SelectedMember.Id);
            }
            else
            {
                MessageBox.Show("Please select a member to view details.", "No Member Selected", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        [RelayCommand]
        private void NavigateToAddMember()
        {
            _navigationService.NavigateTo(nameof(MemberEditViewModel), null); // Pass null or 0 for new member
        }

        [RelayCommand]
        private async Task DeleteMemberAsync()
        {
            if (SelectedMember != null)
            {
                var result = MessageBox.Show($"Are you sure you want to delete {SelectedMember.FullName}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    await _memberService.DeleteAsync(SelectedMember.Id);
                    await LoadMembersAsync(); // Refresh the list
                }
            }
            else
            {
                MessageBox.Show("Please select a member to delete.", "No Member Selected", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
