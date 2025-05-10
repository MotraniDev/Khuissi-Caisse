# System Patterns

## Architecture Pattern
The application follows the Model-View-ViewModel (MVVM) architectural pattern:
- **Models**: Represent data entities (Member, Subscription, Expense, etc.)
- **Views**: XAML-based UI with minimal code-behind
- **ViewModels**: Handle presentation logic and UI state management
- **Services**: Implement business logic and data operations
- **Repositories**: Provide data access abstractions

## Key Design Patterns
1. **Dependency Injection**
   - Constructor injection for services and repositories
   - Microsoft.Extensions.DependencyInjection used as DI container
   - Configured in App.xaml.cs

2. **Repository Pattern**
   - Abstract data access layer
   - Interface-based implementations for testability
   - Entity-specific repositories

3. **Service Layer**
   - Business logic in dedicated services
   - Interface-based for dependency injection
   - Mock implementations for UI development

4. **Command Pattern**
   - MVVM ICommand implementation for UI actions
   - Relay commands for simple action delegation

5. **Observer Pattern**
   - INotifyPropertyChanged for ViewModel-View updates
   - ObservableCollections for dynamically updating collections

## Data Flow
1. User interacts with View
2. View forwards events to ViewModel through commands
3. ViewModel calls appropriate Service methods
4. Service uses Repository for data operations
5. Repository interacts with EF Core DbContext
6. Changes flow back through the chain for UI updates

## Folder Structure
- Models/ - Data entities
- ViewModels/ - Presentation logic
- Views/ - UI definitions
- Services/ - Business logic interfaces and implementations
- Repositories/ - Data access interfaces and implementations
- Properties/ - Resource files and assembly info
- Converters/ - Value converters for UI binding

## Localization Strategy
- Resource files (.resx) for all UI strings
- Culture-specific resources for Arabic
- Converters for locale-specific formatting

## Error Handling
- Structured try-catch blocks
- Service methods return Result<T> pattern
- User-friendly error messages from resources
- Comprehensive logging via Serilog
