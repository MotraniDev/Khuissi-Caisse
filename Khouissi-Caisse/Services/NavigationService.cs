using Khouissi_Caisse.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Khouissi_Caisse.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, Type> _viewModelViewMap = new Dictionary<string, Type>();
        private Frame? _mainFrame; // Made nullable

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Initialize(Frame mainFrame)
        {
            _mainFrame = mainFrame ?? throw new ArgumentNullException(nameof(mainFrame));
        }

        public void RegisterView(string viewModelName, Type viewType)
        {
            if (string.IsNullOrEmpty(viewModelName))
                throw new ArgumentNullException(nameof(viewModelName));
            if (viewType == null)
                throw new ArgumentNullException(nameof(viewType));

            if (!_viewModelViewMap.ContainsKey(viewModelName))
            {
                _viewModelViewMap.Add(viewModelName, viewType);
            }
            else
            {
                // Or update, or throw exception if re-registration is not allowed
                _viewModelViewMap[viewModelName] = viewType;
            }
        }

        public void NavigateTo(string viewModelName, object? parameter = null) // Changed to object? to allow null
        {
            if (_mainFrame == null)
            {
                // Consider logging this or throwing a more specific exception
                // This indicates Initialize was not called or was called with null.
                MessageBox.Show("NavigationService not initialized. MainFrame is null.", "Navigation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }            if (_viewModelViewMap.TryGetValue(viewModelName, out Type? viewType) && viewType != null)
            {
                try
                {
                    // Resolve the view using the service provider if it's registered there,
                    // otherwise, try to activate it directly.
                    // This allows views to have their dependencies injected.
                    object viewInstance = (_serviceProvider.GetService(viewType) ?? Activator.CreateInstance(viewType))!;

                    if (viewInstance is Page page)
                    {
                        // If the view's DataContext is a ViewModel that implements IParameterReceiver (or similar),
                        // pass the parameter. This is a common pattern.
                        if (page.DataContext != null && parameter != null)
                        {
                            // Example: (page.DataContext as IParameterReceiver)?.SetParameter(parameter);
                            // For now, we'll assume the ViewModel handles parameter passing if needed,
                            // perhaps through a constructor or a specific method called after navigation.
                            // If the ViewModel is resolved via DI, its constructor can take parameters.
                            // Or, if the ViewModel is set in XAML, we might need a different approach.

                            // A simple way to pass parameters is to set a property on the ViewModel after it's created.
                            // This requires the ViewModel to be resolved first.
                            var viewModel = page.DataContext;
                            if (viewModel != null)
                            {
                                // This is a placeholder for a more robust parameter passing mechanism.
                                // e.g., if ViewModel has a method LoadState(object parameter)
                                var loadMethod = viewModel.GetType().GetMethod("LoadState"); // Or a specific interface method
                                if (loadMethod != null && loadMethod.GetParameters().Length == 1)
                                {
                                    loadMethod.Invoke(viewModel, new[] { parameter });
                                }
                                else
                                {
                                    // Attempt to set a property if a method isn't found
                                    // This is very basic and might not be suitable for all cases.
                                    var paramProp = viewModel.GetType().GetProperty("NavigationParameter");
                                    if (paramProp != null && paramProp.CanWrite && parameter != null) // Added null check for parameter
                                    {
                                        paramProp.SetValue(viewModel, parameter);
                                    }
                                }
                            }
                        }
                         _mainFrame.Navigate(page);
                    }
                    else
                    {
                        MessageBox.Show($"Registered view '{viewType.Name}' for ViewModel '{viewModelName}' is not a Page.", "Navigation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception
                    MessageBox.Show($"Error navigating to {viewModelName}: {ex.Message}", "Navigation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show($"No view registered for ViewModel '{viewModelName}'.", "Navigation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void GoBack()
        {
            if (_mainFrame?.CanGoBack == true)
            {
                _mainFrame.GoBack();
            }
        }

        // Optional: Implement CanGoBack if needed by INavigationService
        // public bool CanGoBack => _mainFrame?.CanGoBack ?? false;

        // Optional: Implement Navigated event if needed
        // public event Action Navigated;
        // private void OnFrameNavigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        // {
        //     Navigated?.Invoke();
        // }
    }
}
