using Khouissi_Caisse.Models;
using Khouissi_Caisse.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel; // Changed from Microsoft.Toolkit.Mvvm.ComponentModel
using CommunityToolkit.Mvvm.Input; // Changed from Microsoft.Toolkit.Mvvm.Input
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media.Imaging;
using System.Threading.Tasks; // Added for Task
using System.Windows; // Ensure only one instance for MessageBox

namespace Khouissi_Caisse.ViewModels
{
    public partial class MemberEditViewModel : ObservableObject
    {
        private readonly IMemberService _memberService;
        private readonly INavigationService _navigationService;
        private Member? _originalMember;
        private bool _isNewMember = true;

        [ObservableProperty]
        private Member _member = new();

        [ObservableProperty]
        private string _pageTitle = string.Empty;

        [ObservableProperty]
        private BitmapImage? _memberPhoto;

        [ObservableProperty]
        private ObservableCollection<FamilyRelationship> _familyRelationships = new();

        [ObservableProperty]
        private ObservableCollection<Member> _availableMembers = new();

        [ObservableProperty]
        private Member? _selectedFamilyMember;

        [ObservableProperty]
        private string _selectedRelationshipType = string.Empty;

        public ObservableCollection<string> RelationshipTypes { get; } = new ObservableCollection<string>
        {
            "زوج/زوجة", "إبن/إبنة", "أب/أم", "أخ/أخت", "صهر/صهرة", "قريب آخر"
        };

        public MemberEditViewModel(IMemberService memberService, INavigationService navigationService)
        {
            _memberService = memberService ?? throw new ArgumentNullException(nameof(memberService));
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));

            // Initialize collections
            FamilyRelationships = new ObservableCollection<FamilyRelationship>();
            AvailableMembers = new ObservableCollection<Member>();

            // Create new member by default - will be replaced if editing existing
            Member = new Member
            {
                Id = 0,
                IsActive = true,
                BirthDate = DateTime.Now.AddYears(-30)
            };
            
            _isNewMember = true;
            PageTitle = "إضافة عضو جديد";

            // Set default photo
            MemberPhoto = new BitmapImage(new Uri("pack://application:,,,/Resources/default_profile.png", UriKind.Absolute));
            
            // Load available members for relationships
            _ = LoadAvailableMembers(); // Call async method, discard task for constructor
        }

        public async Task LoadMemberAsync(int memberId)
        {
            _originalMember = await _memberService.GetByIdAsync(memberId);
            Member = _originalMember?.Clone() ?? new Member { Id = 0, IsActive = true, BirthDate = DateTime.Now.AddYears(-30) }; // Work on a copy

            if (Member != null && Member.Id != 0)
            {
                PageTitle = string.IsNullOrEmpty(Member.FirstName) ? "تعديل بيانات العضو" : $"تعديل بيانات {Member.FirstName} {Member.LastName}";
                _isNewMember = false;
                LoadMemberPhoto();
                await LoadFamilyRelationships();
                await LoadAvailableMembers();
            }
            else // New member or failed to load
            {
                 Member = new Member { Id = 0, IsActive = true, BirthDate = DateTime.Now.AddYears(-30) };
                _isNewMember = true;
                PageTitle = "إضافة عضو جديد";
                LoadMemberPhoto(); // Load default photo
                FamilyRelationships.Clear();
                await LoadAvailableMembers();
            }
        }

        public async Task CreateNewMemberAsync() // Renamed to avoid confusion and allow async
        {
            _originalMember = new Member { Id = 0 }; 
            Member = new Member { Id = 0, IsActive = true, BirthDate = DateTime.Now.AddYears(-30) };
            _isNewMember = true;
            PageTitle = "إضافة عضو جديد";
            LoadMemberPhoto();
            FamilyRelationships.Clear();
            await LoadAvailableMembers();
        }

        private void LoadMemberPhoto()
        {
            if (!string.IsNullOrEmpty(Member.PhotoPath))
            {
                try
                {
                    MemberPhoto = new BitmapImage(new Uri(Member.PhotoPath));
                }
                catch
                {
                    // If loading fails, use a default photo
                    MemberPhoto = new BitmapImage(new Uri("pack://application:,,,/Resources/default_profile.png", UriKind.Absolute));
                }
            }
            else
            {
                MemberPhoto = new BitmapImage(new Uri("pack://application:,,,/Resources/default_profile.png", UriKind.Absolute));
            }
        }

        private async Task LoadFamilyRelationships() // Made async
        {
            FamilyRelationships.Clear();
            
            if (Member != null && Member.Id != 0) // Ensure Member and Member.Id is valid
            {
                var relationships = await _memberService.GetFamilyMembersAsync(Member.Id); // Corrected method name and added await
                if (relationships != null)
                {
                    foreach (var relationship in relationships)
                    {
                        FamilyRelationships.Add(relationship);
                    }
                }
            }
        }

        private async Task LoadAvailableMembers() // Made async
        {
            AvailableMembers.Clear();
            
            // Get all members except current one
            var members = await _memberService.GetAllAsync(); // Corrected method name and added await
            
            if (members == null) return;

            foreach (var memberItem in members) // Renamed to avoid conflict with Member property
            {
                // Skip if this is the current member being edited
                if (!_isNewMember && Member != null && memberItem.Id == Member.Id)
                {
                    continue;
                }
                
                // Skip if already in relationships
                bool alreadyInRelationships = false;
                if (Member != null) // Ensure Member is not null
                {
                    foreach (var relationship in FamilyRelationships)
                    {
                        if (relationship.RelatedMember != null && relationship.RelatedMember.Id == memberItem.Id)
                        {
                            alreadyInRelationships = true;
                            break;
                        }
                    }
                }
                
                if (!alreadyInRelationships)
                {
                    AvailableMembers.Add(memberItem);
                }
            }
        }

        [RelayCommand]
        private async Task SaveMemberAsync()
        {
            if (Member != null) // Replaced EditableMember with Member
            {
                bool success = false;
                if (Member.Id == 0) // New member // Replaced EditableMember with Member
                {
                    var addedMember = await _memberService.AddAsync(Member); // Replaced EditableMember with Member
                    if (addedMember != null)
                    {
                        success = true;
                        Member = addedMember; // Update Member with the returned member
                    }
                }
                else // Existing member
                {
                    success = await _memberService.UpdateAsync(Member); // Replaced EditableMember with Member
                }

                if (success)
                {
                    // Navigate to details view after saving
                    _navigationService.NavigateTo(nameof(MemberDetailsViewModel), Member.Id);
                }
                else
                {
                    MessageBox.Show("Failed to save member data.", "Save Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("No member data to save.", "Save Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateMember()
        {
            if (string.IsNullOrWhiteSpace(Member.FirstName))
            {
                MessageBox.Show("يرجى إدخال الإسم", "خطأ في البيانات", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(Member.LastName))
            {
                MessageBox.Show("يرجى إدخال اللقب", "خطأ في البيانات", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            
            // Add more validation as needed
            
            return true;
        }

        [RelayCommand]
        private void CancelEdit()
        {
            // If it was an existing member, navigate to its details, otherwise to list
            if (_originalMember != null && _originalMember.Id != 0)
            {
                _navigationService.NavigateTo(nameof(MemberDetailsViewModel), _originalMember.Id);
            }
            else
            {
                _navigationService.NavigateTo(nameof(MemberListViewModel));
            }
        }

        [RelayCommand]
        private void OnChangePhoto()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png"
            };

            if (dialog.ShowDialog() == true)
            {
                Member.PhotoPath = dialog.FileName;
                LoadMemberPhoto();
            }
        }

        [RelayCommand(CanExecute = nameof(CanAddFamilyRelation))]
        private async Task OnAddFamilyRelation()
        {
            if (SelectedFamilyMember != null && !string.IsNullOrEmpty(SelectedRelationshipType) && Member != null && Member.Id != 0)
            {
                await _memberService.AddFamilyRelationshipAsync(Member.Id, SelectedFamilyMember.Id, SelectedRelationshipType);
                await LoadFamilyRelationships(); // Refresh the list
                await LoadAvailableMembers(); // Refresh available members as well
            }
        }
        
        private bool CanAddFamilyRelation()
        {
            return SelectedFamilyMember != null && !string.IsNullOrEmpty(SelectedRelationshipType) && Member != null && Member.Id != 0;
        }

        [RelayCommand]
        private async Task OnRemoveFamilyRelation(FamilyRelationship? relationship)
        {
            if (relationship != null && Member != null && Member.Id != 0 && relationship.RelatedMember != null) // Added null check for RelatedMember
            {
                await _memberService.RemoveFamilyRelationshipAsync(Member.Id, relationship.RelatedMemberId);
                await LoadFamilyRelationships(); // Refresh the list
                await LoadAvailableMembers(); // Refresh available members as well
            }
        }

        // Removed redundant Save() and Cancel() methods as SaveMemberAsync() and CancelEdit() are used.
    }
}
