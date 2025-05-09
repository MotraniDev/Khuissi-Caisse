namespace Khouissi_Caisse.Services.Interfaces
{
    public interface INavigationService
    {
        void NavigateTo(string viewModelName, object? parameter = null); // Changed to object? to allow null
        void GoBack();
        // Add other navigation methods as needed, e.g.:
        // bool CanGoBack { get; }
        // event Action Navigated;
        // void NavigateToRoot();
        // void RegisterView(string viewModelName, Type viewType);
    }
}
