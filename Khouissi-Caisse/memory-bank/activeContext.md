# Active Context - نظام إدارة الصندوق

## Current Focus

The نظام إدارة الصندوق (Subscription Management Fund) project is currently in active development with a focus on implementing a fully functional WPF application with Arabic language support and RTL (Right-to-Left) interface. The development follows a UI/UX-first approach with mocked services that will be progressively replaced with actual implementations.

## Recent Changes

1. **Arabic Localization & RTL Setup**:
   - Configured application for Arabic language support
   - Set up RTL layout for the interface
   - Added custom Arabic font (Alarabiya-Font.ttf)
   - Configured Western digits (0-9) for numerical values

2. **Authentication System**:
   - Implemented user service interface and mock implementation
   - Created login view with Arabic UI
   - Set up password security handling
   - Simplified login view (removed Remember Me option and loading indicator)

3. **Application Shell & Navigation**:
   - Built main application shell with RTL support
   - Implemented navigation service for view transitions
   - Configured dependency injection container

4. **Core Model Development**:
   - Implemented Member entity model
   - Implemented User entity model
   - Progress on Subscription and Expense models

## Next Steps

1. **Complete Core Data Models**:
   - Finalize Subscription entity model
   - Complete Expense entity model
   - Implement ApplicationSettings entity model

2. **Repository Layer Implementation**:
   - Create base Repository interface and generic implementation
   - Implement specific repositories for entities
   - Set up data persistence mechanisms

3. **Business Service Implementation**:
   - Replace mock services with actual implementations
   - Implement expense service
   - Create reporting service with PDF generation capability

4. **UI Refinement**:
   - Complete member management views
   - Implement subscription management UI components
   - Develop expense management interfaces
   - Create reporting views

## Active Decisions & Considerations

1. **Service Mocking Strategy**:
   - Currently using mock services to facilitate UI development
   - Each mock service implements the corresponding interface
   - Mock services provide realistic but static data

2. **Data Storage Approach**:
   - Planning XML-based configuration for application settings
   - Evaluating options for local data storage (SQLite, LiteDB, etc.)
   - Considering backup and data migration capabilities

3. **Authentication Method**:
   - Implementing simple username/password authentication
   - Password security with hashing
   - Role-based authorization for potential future user types

## Important Patterns & Preferences

1. **MVVM Implementation**:
   - Views should have minimal code-behind
   - ViewModels handle UI logic and service interactions
   - Models represent pure domain entities

2. **Service Design**:
   - Services should be interface-based for testability
   - Services access data through repository abstractions
   - Business logic centralized in services, not in ViewModels

3. **UI Development Approach**:
   - Right-to-Left layout throughout the application
   - Arabic text in the interface with proper font support
   - Western digits for numerical values
   - Consistency in UI component placement and behavior

## Project Insights

1. **Arabic RTL Challenges**:
   - Special attention needed for text alignment in XAML
   - Date formatting needs to respect Arabic cultural preferences
   - Input fields must properly support Arabic text entry

2. **Development Progress**:
   - Using UI-first approach with mocked services is proving effective
   - Clear separation of concerns through MVVM helps manage complexity
   - Dependency injection provides flexibility for service replacement

3. **Future Considerations**:
   - May need to implement data migration tools for version updates
   - Consider export/import functionality for backup purposes
   - Evaluate potential for multi-user concurrent access in future versions
