using System.Configuration;
using System.Data;
using System.Windows;
using System.Threading;
using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using Khouissi_Caisse.Services;
using Khouissi_Caisse.ViewModels;
using Khouissi_Caisse.Services.Interfaces;
using Khouissi_Caisse.Views; // Add this for View types

namespace Khouissi_Caisse;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static IServiceProvider ServiceProvider { get; private set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        // Your startup code here

        // Set Arabic culture with Western digits (0-9)
        var arabicCulture = new CultureInfo("ar");
        arabicCulture.NumberFormat.DigitSubstitution = DigitShapes.None; // Use Western digits (0-9)

        // Set culture for current thread and default thread culture
        Thread.CurrentThread.CurrentCulture = arabicCulture;
        Thread.CurrentThread.CurrentUICulture = arabicCulture;
        CultureInfo.DefaultThreadCurrentCulture = arabicCulture;
        CultureInfo.DefaultThreadCurrentUICulture = arabicCulture;

        // Configure dependency injection
        var services = new ServiceCollection();
        ConfigureServices(services);
        ServiceProvider = services.BuildServiceProvider();
    }

    /// <summary>
    /// Configures the application's dependency injection container
    /// </summary>
    private void ConfigureServices(IServiceCollection services)
    {          // Register services        
        services.AddSingleton<IUserService, MockUserService>();
        services.AddSingleton<ISubscriptionService, MockSubscriptionService>();
        services.AddSingleton<Khouissi_Caisse.Services.Interfaces.IMemberService, MockMemberService>();
        
        // NavigationService registration
        services.AddSingleton<INavigationService, NavigationService>(sp =>
        {
            var navigationService = new NavigationService(sp);
            // Register views with the navigation service
            // Ensure these view classes exist in the Khouissi_Caisse.Views namespace
            navigationService.RegisterView(nameof(LoginViewModel), typeof(LoginView)); // Assuming LoginView exists
            navigationService.RegisterView(nameof(MemberListViewModel), typeof(MemberListView));
            navigationService.RegisterView(nameof(MemberDetailsViewModel), typeof(MemberDetailsView));
            navigationService.RegisterView(nameof(MemberEditViewModel), typeof(MemberEditView));
            // Add other view registrations here
            return navigationService;
        });

        // Register ViewModels
        services.AddTransient<LoginViewModel>();
        services.AddTransient<MemberListViewModel>();
        services.AddTransient<MemberDetailsViewModel>();
        services.AddTransient<MemberEditViewModel>();

        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainViewModel>();

        services.AddTransient<LoginView>();
        services.AddTransient<LoginViewModel>();

        services.AddTransient<MemberListView>();
        services.AddTransient<MemberListViewModel>();

        services.AddTransient<MemberEditView>();
        services.AddTransient<MemberEditViewModel>();

        services.AddTransient<HomeView>(); // Add this
        services.AddTransient<HomeViewModel>(); // Add this

        // In the future, we'll register other services like this:
        // services.AddSingleton<IExpenseService, MockExpenseService>();

        // In the future, we'll register other ViewModels like this:
        // services.AddTransient<MemberDetailsViewModel>();
        // services.AddTransient<MemberEditViewModel>();

        _serviceProvider = services.BuildServiceProvider();

        // Register Views with NavigationService
        var navigationService = _serviceProvider.GetRequiredService<NavigationService>();
        navigationService.RegisterView<LoginView, LoginViewModel>();
        navigationService.RegisterView<MemberListView, MemberListViewModel>();
        navigationService.RegisterView<MemberEditView, MemberEditViewModel>();
        navigationService.RegisterView<HomeView, HomeViewModel>(); // Add this
    }
}

