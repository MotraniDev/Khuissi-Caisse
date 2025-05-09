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
                {                    // Resolve the view using the service provider if it's registered there,
                    // otherwise, try to activate it directly.
                    // This allows views to have their dependencies injected.
                    object viewInstance = (_serviceProvider.GetService(viewType) ?? Activator.CreateInstance(viewType))!;

                    if (viewInstance is Page page)
                    {
                        // Resolve the corresponding ViewModel for this view
                        // viewModelName is already the name of the ViewModel type (e.g., "LoginViewModel")
                        var viewModelType = Type.GetType($"Khouissi_Caisse.ViewModels.{viewModelName}") ?? 
                            Type.GetType($"Khouissi_Caisse.ViewModels.{viewModelName}, Khouissi-Caisse");

                        if (viewModelType != null)
                        {
                            // Get the ViewModel from the service provider
                            var viewModel = _serviceProvider.GetService(viewModelType);
                            
                            // Set the DataContext of the page to the ViewModel
                            if (viewModel != null)
                            {
                                page.DataContext = viewModel;
                                
                                // Handle parameter passing if needed
                                if (parameter != null)
                                {
                                    // Check if the ViewModel has a LoadMemberAsync or LoadState method
                                    var loadMethod = viewModelType.GetMethod("LoadMemberAsync") ?? 
                                                    viewModelType.GetMethod("LoadState");
                                    
                                    if (loadMethod != null && loadMethod.GetParameters().Length == 1)
                                    {
                                        loadMethod.Invoke(viewModel, new[] { parameter });
                                    }
                                    else
                                    {
                                        // Try to set a parameter property
                                        var paramProp = viewModelType.GetProperty("NavigationParameter");
                                        if (paramProp != null && paramProp.CanWrite)
                                        {
                                            paramProp.SetValue(viewModel, parameter);
                                        }
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
