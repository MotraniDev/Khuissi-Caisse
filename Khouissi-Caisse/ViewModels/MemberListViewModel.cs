using Khouissi_Caisse.Models;
using Khouissi_Caisse.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Khouissi_Caisse.ViewModels;

/// <summary>
/// ViewModel for the member list view
/// </summary>
public class MemberListViewModel : ViewModelBase
{
    private readonly IMemberService _memberService;
    private string _searchText = string.Empty;
    private bool _isLoading;
    private string _errorMessage = string.Empty;
    private Member? _selectedMember;

    /// <summary>
    /// Collection of members to display
    /// </summary>
    public ObservableCollection<Member> Members { get; } = new ObservableCollection<Member>();

    /// <summary>
    /// Text used for searching members
    /// </summary>
    public string SearchText
    {
        get => _searchText;
        set
        {
            if (SetProperty(ref _searchText, value))
            {
                // We could trigger search on text changed, but for better performance
                // we'll wait for the search command to be executed
                SearchCommand.RaiseCanExecuteChanged();
            }
        }
    }

    /// <summary>
    /// Indicates if data is being loaded
    /// </summary>
    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    /// <summary>
    /// Error message to display if operations fail
    /// </summary>
    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }

    /// <summary>
    /// Currently selected member in the list
    /// </summary>
    public Member? SelectedMember
    {
        get => _selectedMember;
        set
        {
            if (SetProperty(ref _selectedMember, value))
            {
                // Update command availability
                EditMemberCommand.RaiseCanExecuteChanged();
                DeleteMemberCommand.RaiseCanExecuteChanged();
                ViewMemberCommand.RaiseCanExecuteChanged();
                MemberSelected?.Invoke(this, value);
            }
        }
    }

    /// <summary>
    /// Command to search for members
    /// </summary>
    public RelayCommand SearchCommand { get; }

    /// <summary>
    /// Command to refresh the member list
    /// </summary>
    public RelayCommand RefreshCommand { get; }

    /// <summary>
    /// Command to add a new member
    /// </summary>
    public RelayCommand AddMemberCommand { get; }

    /// <summary>
    /// Command to edit the selected member
    /// </summary>
    public RelayCommand EditMemberCommand { get; }

    /// <summary>
    /// Command to delete the selected member
    /// </summary>
    public RelayCommand DeleteMemberCommand { get; }

    /// <summary>
    /// Command to view details of the selected member
    /// </summary>
    public RelayCommand ViewMemberCommand { get; }

    /// <summary>
    /// Event raised when a member is selected
    /// </summary>
    public event EventHandler<Member?>? MemberSelected;

    /// <summary>
    /// Event raised when a member should be edited
    /// </summary>
    public event EventHandler<Member?>? EditMemberRequested;

    /// <summary>
    /// Event raised when a new member should be added
    /// </summary>
    public event EventHandler? AddMemberRequested;

    /// <summary>
    /// Event raised when member details should be viewed
    /// </summary>
    public event EventHandler<Member?>? ViewMemberRequested;

    /// <summary>
    /// Creates a new MemberListViewModel
    /// </summary>
    /// <param name="memberService">Service for member operations</param>
    public MemberListViewModel(IMemberService memberService)
    {
        _memberService = memberService ?? throw new ArgumentNullException(nameof(memberService));

        // Initialize commands
        SearchCommand = new RelayCommand(async _ => await SearchAsync(), _ => !IsLoading && !string.IsNullOrEmpty(SearchText));
        RefreshCommand = new RelayCommand(async _ => await LoadMembersAsync(), _ => !IsLoading);
        AddMemberCommand = new RelayCommand(_ => OnAddMemberRequested(), _ => !IsLoading);
        EditMemberCommand = new RelayCommand(_ => OnEditMemberRequested(), _ => !IsLoading && SelectedMember != null);
        DeleteMemberCommand = new RelayCommand(async _ => await DeleteMemberAsync(), _ => !IsLoading && SelectedMember != null);
        ViewMemberCommand = new RelayCommand(_ => OnViewMemberRequested(), _ => !IsLoading && SelectedMember != null);

        // Load members on initialization
        _ = LoadMembersAsync();
    }

    /// <summary>
    /// Loads all members
    /// </summary>
    private async Task LoadMembersAsync()
    {
        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var members = await _memberService.GetAllAsync();

            // Update the collection on the UI thread
            Application.Current.Dispatcher.Invoke(() =>
            {
                Members.Clear();
                foreach (var member in members.OrderBy(m => m.FirstName).ThenBy(m => m.LastName))
                {
                    Members.Add(member);
                }
            });
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error loading members: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    /// <summary>
    /// Searches for members
    /// </summary>
    private async Task SearchAsync()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            await LoadMembersAsync();
            return;
        }

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var members = await _memberService.SearchAsync(SearchText);

            // Update the collection on the UI thread
            Application.Current.Dispatcher.Invoke(() =>
            {
                Members.Clear();
                foreach (var member in members.OrderBy(m => m.FirstName).ThenBy(m => m.LastName))
                {
                    Members.Add(member);
                }
            });
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error searching for members: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    /// <summary>
    /// Deletes the selected member
    /// </summary>
    private async Task DeleteMemberAsync()
    {
        if (SelectedMember == null)
            return;

        var result = MessageBox.Show(
            $"هل أنت متأكد من حذف العضو {SelectedMember.FullName}؟",
            "تأكيد الحذف",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question,
            MessageBoxResult.No);

        if (result != MessageBoxResult.Yes)
            return;

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            await _memberService.DeleteAsync(SelectedMember.Id);

            // Remove from collection
            Application.Current.Dispatcher.Invoke(() =>
            {
                Members.Remove(SelectedMember);
                SelectedMember = null;
            });

            MessageBox.Show("تم حذف العضو بنجاح", "نجاح", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error deleting member: {ex.Message}";
            MessageBox.Show($"حدث خطأ أثناء حذف العضو: {ex.Message}", "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            IsLoading = false;
        }
    }

    /// <summary>
    /// Raises the AddMemberRequested event
    /// </summary>
    private void OnAddMemberRequested()
    {
        AddMemberRequested?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Raises the EditMemberRequested event
    /// </summary>
    private void OnEditMemberRequested()
    {
        if (SelectedMember != null)
        {
            EditMemberRequested?.Invoke(this, SelectedMember);
        }
    }

    /// <summary>
    /// Raises the ViewMemberRequested event
    /// </summary>
    private void OnViewMemberRequested()
    {
        if (SelectedMember != null)
        {
            ViewMemberRequested?.Invoke(this, SelectedMember);
        }
    }
}
