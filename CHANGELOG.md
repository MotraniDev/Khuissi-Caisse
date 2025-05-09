# Changelog

All notable changes to the project will be documented in this file.

## [Unreleased]

### Added

- fix(login): Fixed login functionality with default "123" password
  - Updated NavigationService to properly set DataContext for views
  - Enhanced MockUserService to correctly handle default password authentication
  - Added multiple login triggers (Button click, Enter key) for better UX
  - Fixed command execution in LoginViewModel
  - Improved login feedback with diagnostic messages

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
- feat(member): Started member management implementation
  - Created IMemberService interface with core member management operations
  - Implemented MockMemberService for UI development
  - Added ActiveStatusConverter for visual member status representation
  - Created initial MemberListView and MemberListViewModel

- Implemented Member management UI components:
  - MemberDetailsView and ViewModel to display member information
  - MemberEditView and ViewModel for adding/editing member data
  - Family relationship management in member editing
- Created MockMemberService implementing IMemberService interface
- Created MockSubscriptionService implementing ISubscriptionService interface
- Added ActiveStatusConverter for displaying member status
- Enhanced Member model with FullName property and Clone method
- Added FamilyRelationship model for representing family connections

- feat(ui): Updated MainWindow.xaml navigation buttons and user info section
  - Added Arabic labels for navigation buttons
  - Styled user info section with dynamic resource for login button

### Changed

- Updated MainWindow.xaml to integrate the login functionality
- Enhanced App.xaml to include Arabic string resources and default RTL styles
- Updated App.xaml.cs to register new services and ViewModels
- Updated Project-Todo.md to mark completed tasks

### Fixed

- Fixed LoginView.xaml.cs to correctly handle PasswordBox binding
- Ensured proper Arabic text display with Western digits (0-9)
- Fixed RTL layout issues in member management views
