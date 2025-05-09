# نظام إدارة الصندوق - Changelog

All notable changes to the project will be documented in this file.

## [Unreleased]

### Added

- feat(login): Implemented login functionality with Arabic UI and RTL support
  - Created LoginView with Arabic UI elements and RTL layout
  - Implemented LoginViewModel with full authentication flow
  - Added password security handling for PasswordBox
  - Created value converters (BoolToVisibilityConverter, StringToVisibilityConverter)
  - Added Arabic string resources in App.xaml
  - Set up proper RTL styling with Arabic fonts throughout the application

### Changed

- Updated MainWindow.xaml to integrate the login functionality
- Enhanced App.xaml to include Arabic string resources and default RTL styles

### Fixed

- Fixed LoginView.xaml.cs to correctly handle PasswordBox binding
- Ensured proper Arabic text display with Western digits (0-9)
