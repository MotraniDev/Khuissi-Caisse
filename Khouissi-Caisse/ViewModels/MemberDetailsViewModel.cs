using Khouissi_Caisse.Models;
using Khouissi_Caisse.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel; // Changed from Microsoft.Toolkit.Mvvm.ComponentModel
using CommunityToolkit.Mvvm.Input; // Changed from Microsoft.Toolkit.Mvvm.Input
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Threading.Tasks; // Added for Task

namespace Khouissi_Caisse.ViewModels
{
    public partial class MemberDetailsViewModel : ObservableObject
    {
        private readonly IMemberService _memberService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private Member? _member;

        private BitmapImage _memberPhoto = new BitmapImage(new Uri("pack://application:,,,/Resources/default_profile.png", UriKind.Absolute)); // Initialize with default
        public BitmapImage MemberPhoto
        {
            get => _memberPhoto;
            set => SetProperty(ref _memberPhoto, value);
        }

        public string MemberFullName => $"{Member?.FirstName} {Member?.LastName}";

        private DateTime? _lastPaymentDate;
        public DateTime? LastPaymentDate
        {
            get => _lastPaymentDate;
            set => SetProperty(ref _lastPaymentDate, value);
        }

        private string _subscriptionStatus = string.Empty; // Initialize
        public string SubscriptionStatus
        {
            get => _subscriptionStatus;
            set => SetProperty(ref _subscriptionStatus, value);
        }

        private Brush _subscriptionStatusColor = Brushes.Gray; // Initialize
        public Brush SubscriptionStatusColor
        {
            get => _subscriptionStatusColor;
            set => SetProperty(ref _subscriptionStatusColor, value);
        }

        private decimal _advancePaymentAmount;
        public decimal AdvancePaymentAmount
        {
            get => _advancePaymentAmount;
            set => SetProperty(ref _advancePaymentAmount, value);
        }

        private string _familyRelationshipText = string.Empty; // Initialize
        public string FamilyRelationshipText
        {
            get => _familyRelationshipText;
            set => SetProperty(ref _familyRelationshipText, value);
        }

        private Brush _notesTextColor = Brushes.Gray; // Initialize
        public Brush NotesTextColor
        {
            get => _notesTextColor;
            set => SetProperty(ref _notesTextColor, value);
        }

        public MemberDetailsViewModel(IMemberService memberService, ISubscriptionService subscriptionService, INavigationService navigationService)
        {
            _memberService = memberService ?? throw new ArgumentNullException(nameof(memberService));
            _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));

            // Default values until we have real data
            Member = new Member(); // This will be overwritten by LoadMemberAsync
            // _memberPhoto is already initialized
            // _subscriptionStatus is already initialized
            // _subscriptionStatusColor is already initialized
            // _familyRelationshipText is already initialized
            // _notesTextColor is already initialized
            LastPaymentDate = DateTime.Now.AddMonths(-1); 
            AdvancePaymentAmount = 0;
        }

        public async Task LoadMemberAsync(int memberId)
        {
            try
            {
                Member = await _memberService.GetByIdAsync(memberId);
                
                if (Member != null)
                {
                    OnPropertyChanged(nameof(MemberFullName));
                    LoadMemberPhoto(); // This is synchronous
                    LoadSubscriptionData(); // This is synchronous
                    await LoadFamilyRelationshipsAsync(); // Changed to await async version
                    
                    NotesTextColor = Member != null && string.IsNullOrEmpty(Member.Notes) ? Brushes.Gray : Brushes.Black;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحميل بيانات العضو: {ex.Message}", "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadMemberPhoto()
        {
            if (Member != null && !string.IsNullOrEmpty(Member.PhotoPath))
            {
                try
                {
                    MemberPhoto = new BitmapImage(new Uri(Member.PhotoPath));
                }
                catch
                {
                    MemberPhoto = new BitmapImage(new Uri("pack://application:,,,/Resources/default_profile.png", UriKind.Absolute));
                }
            }
            else
            {
                MemberPhoto = new BitmapImage(new Uri("pack://application:,,,/Resources/default_profile.png", UriKind.Absolute));
            }
        }

        private void LoadSubscriptionData()
        {
            if (Member == null) 
            {
                SubscriptionStatus = "غير متوفر";
                SubscriptionStatusColor = Brushes.Gray;
                LastPaymentDate = null;
                AdvancePaymentAmount = 0;
                return; 
            }
            // In a real implementation, this would load from the subscription service
            // For now, we'll use mock data
            LastPaymentDate = _subscriptionService.GetLastPaymentDate(Member.Id);
            var status = _subscriptionService.GetSubscriptionStatus(Member.Id);
            
            SubscriptionStatus = status.Status;
            SubscriptionStatusColor = status.IsActive ? Brushes.Green : Brushes.Red;
            AdvancePaymentAmount = _subscriptionService.GetAdvancePaymentAmount(Member.Id);
        }

        private async Task LoadFamilyRelationshipsAsync() // Changed to async Task
        {
            if (Member == null) // Added null check
            {
                FamilyRelationshipText = "لا توجد معلومات عضو لتحميل الروابط العائلية";
                return;
            }

            var familyMembers = await _memberService.GetFamilyMembersAsync(Member.Id);
            
            if (familyMembers != null && familyMembers.Count > 0)
            {
                var relationshipText = "روابط العائلة: ";
                foreach (var relation in familyMembers)
                {
                    if (relation.RelatedMember != null)
                    {
                        relationshipText += $"{relation.RelatedMember.FirstName} {relation.RelatedMember.LastName} ({relation.RelationshipType}), ";
                    }
                    else
                    {
                        relationshipText += $"[عضو غير معروف] ({relation.RelationshipType}), ";
                    }
                }
                FamilyRelationshipText = relationshipText.TrimEnd(',', ' ');
            }
            else
            {
                FamilyRelationshipText = "لا توجد روابط عائلية مسجلة";
            }
        }

        [RelayCommand]
        private void NavigateToEditMember()
        {
            if (Member != null)
            {
                _navigationService.NavigateTo(nameof(MemberEditViewModel), Member.Id);
            }
        }

        [RelayCommand]
        private void GoBack()
        {
            _navigationService.NavigateTo(nameof(MemberListViewModel));
        }
    }
}
