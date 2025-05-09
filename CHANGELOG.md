# نظام إدارة الصندوق - Changelog

All notable changes to the project will be documented in this file.

## [Unreleased]

### Added

- feat(login): Simplified to password-only authentication
  - Modified IUserService to support password-only authentication
  - Updated MockUserService implementation
  - Simplified login UI to only show password field
  - Updated LoginViewModel to handle password-only login flow
  - Improved focus handling for password input

- feat(login): Implemented login functionality with Arabic UI and RTL support
  - Created LoginView with Arabic UI elements and RTL layout
  - Implemented LoginViewModel with full authentication flow
  - Added password security handling for PasswordBox
  - Created value converters (BoolToVisibilityConverter, StringToVisibilityConverter)
  - Added Arabic string resources in App.xaml
  - Set up proper RTL styling with Arabic fonts throughout the application

- feat(core): Implemented core data models and repositories
  - Created entity models (Member, Subscription, Expense, ApplicationSettings)
  - Implemented generic repository pattern (IRepository<T> and Repository<T>)
  - Added specific repositories for each entity with additional methods
  - Set up database context with relationships between entities

### Changed

- Updated MainWindow.xaml to integrate the login functionality
- Enhanced App.xaml to include Arabic string resources and default RTL styles

### Fixed

- Fixed LoginView.xaml.cs to correctly handle PasswordBox binding
- Ensured proper Arabic text display with Western digits (0-9)
