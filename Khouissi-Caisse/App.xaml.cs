using System.Configuration;
using System.Data;
using System.Windows;
using System.Threading;
using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using Khouissi_Caisse.Services;
using Khouissi_Caisse.ViewModels;

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
    {        // Register services
        services.AddSingleton<IUserService, MockUserService>();
        services.AddSingleton<IMemberService, MockMemberService>();

        // In the future, we'll register other services like this:
        // services.AddSingleton<ISubscriptionService, MockSubscriptionService>();
        // services.AddSingleton<IExpenseService, MockExpenseService>();        // Register ViewModels
        services.AddTransient<LoginViewModel>();
        services.AddTransient<MemberListViewModel>();

        // In the future, we'll register other ViewModels like this:
        // services.AddTransient<MemberDetailsViewModel>();
        // services.AddTransient<MemberEditViewModel>();
    }
}

