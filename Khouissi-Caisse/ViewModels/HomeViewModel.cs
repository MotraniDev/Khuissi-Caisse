using System.Collections.ObjectModel;
using Khouissi_Caisse.Models; // Assuming you might add some summary data models later

namespace Khouissi_Caisse.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {        private string _welcomeMessage = string.Empty;
        public string WelcomeMessage
        {
            get => _welcomeMessage;
            set => SetProperty(ref _welcomeMessage, value);
        }

        // Placeholder for future summary data
        // public ObservableCollection<SummaryItem> SummaryItems { get; }
        
        public HomeViewModel()
        {
            WelcomeMessage = "مرحبًا بك في نظام إدارة الصندوق"; // Default welcome message in Arabic

            // SummaryItems = new ObservableCollection<SummaryItem>();
            // LoadSummaryData();
        }

        // private void LoadSummaryData()
        // {
        //     // In a real scenario, fetch data from a service
        //     // For now, mock data or leave empty
        //     SummaryItems.Add(new SummaryItem { Title = "إجمالي الأعضاء", Value = "0" });
        //     SummaryItems.Add(new SummaryItem { Title = "اشتراكات الشهر الحالي", Value = "0 د.ج" });
        //     SummaryItems.Add(new SummaryItem { Title = "مصروفات الشهر الحالي", Value = "0 د.ج" });
        // }
    }

    // Placeholder for a summary item model
    // public class SummaryItem
    // {
    //     public string Title { get; set; }
    //     public string Value { get; set; }
    // }
}