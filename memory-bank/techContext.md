# Technical Context

## Development Environment
- **.NET Version**: .NET 6.0
- **UI Framework**: Windows Presentation Foundation (WPF)
- **IDE**: Visual Studio or VS Code with extensions
- **Target OS**: Windows 10/11

## Frontend Technologies
- **Markup Language**: XAML for UI definition
- **UI Pattern**: MVVM (Model-View-ViewModel)
- **UI Toolkit**: WPF built-in controls
- **Styling**: Custom XAML styles and resources
- **Localization**: ResourceDictionary and .resx files for Arabic support

## Backend Technologies
- **Language**: C# 10.0
- **ORM**: Entity Framework Core 6.0
- **Database**: SQLite (local file-based)
- **Security**: BCrypt for password hashing
- **Logging**: Serilog for structured logging

## Key Dependencies
- **Microsoft.EntityFrameworkCore.Sqlite**: Database access
- **BCrypt.Net-Next**: Secure password hashing
- **Microsoft.Extensions.DependencyInjection**: DI container
- **CommunityToolkit.Mvvm**: MVVM helpers
- **Serilog**: Logging infrastructure

## Configuration
- **App Settings**: Local JSON configuration
- **Database**: Local SQLite file
- **Logging**: File and console outputs

## Technical Constraints
- **RTL Support**: All UI must be designed for Arabic RTL layout
- **Localization**: All strings must be in resource files for Arabic support
- **Fonts**: Must use fonts with good Arabic script support
- **Numbers**: Western digits (0-9) for numeric content
- **Performance**: Responsive UI even with large datasets
- **Offline Operation**: Must function without internet connection
