# Login Flow Documentation

This document outlines the detailed flow of the login system for the Khouissi-Caisse application.

## Login Flow Diagram

```mermaid
flowchart TD
    Start([Start]) --> PasswordEntry[User enters password]
    PasswordEntry --> LoginAttempt[User initiates login]
    LoginAttempt --> LoginCommand[LoginCommand executed]
    LoginCommand --> CanLogin{Can Login?}
    CanLogin -->|No| ErrorMessage[Display Error]
    CanLogin -->|Yes| LoginProcess[LoginAsync called]
    LoginProcess --> Authenticate[Authentication]
    Authenticate --> PasswordCheck{Valid password?}
    PasswordCheck -->|Yes| UserFound[Return User]
    PasswordCheck -->|No| UserNotFound[Return null]
    UserFound --> LoginSuccess[Success]
    UserNotFound --> LoginFail[Failed]
    LoginSuccess --> RaiseEvent[Raise event]
    LoginFail --> SetError[Set error]
    RaiseEvent --> Navigate[Navigate]
    Navigate --> EndSuccess([End])
    SetError --> EndFail([End])

    classDef processStyle fill:#f9f,stroke:#333,stroke-width:1px
    classDef decisionStyle fill:#bbf,stroke:#333,stroke-width:1px
    classDef startStyle fill:#9f9,stroke:#333,stroke-width:1px
    classDef endStyle fill:#f99,stroke:#333,stroke-width:1px
    
    class Start,EndSuccess,EndFail startStyle
    class CanLogin,PasswordCheck decisionStyle
    class LoginProcess,Authenticate,UserFound,UserNotFound,LoginSuccess,LoginFail processStyle
```

## Detailed Login Flow

```mermaid
sequenceDiagram
    participant User
    participant LoginView
    participant LoginViewModel
    participant UserService
    participant NavigationService
    
    User->>LoginView: Enter Password
    User->>LoginView: Click Login Button
    LoginView->>LoginViewModel: Execute LoginCommand
    LoginViewModel->>LoginViewModel: Check CanLogin()
    
    alt Can Login
        LoginViewModel->>LoginViewModel: Clear ErrorMessage
        LoginViewModel->>LoginViewModel: Set IsLoading = true
        LoginViewModel->>UserService: AuthenticateWithPasswordOnlyAsync(password)
        UserService->>UserService: Check password equals "123"
        
        alt Valid Password
            UserService-->>LoginViewModel: Return User object
            LoginViewModel->>LoginViewModel: Raise LoginSuccessful event
            LoginViewModel->>NavigationService: Navigate to Main View
            NavigationService-->>User: Display Main Application
        else Invalid Password
            UserService-->>LoginViewModel: Return null
            LoginViewModel->>LoginViewModel: Set ErrorMessage
            LoginViewModel-->>User: Display Error Message
        end
        
        LoginViewModel->>LoginViewModel: Set IsLoading = false
    else Cannot Login
        LoginViewModel-->>User: No action
    end
```

## Detailed Component Description

### 1. UI Layer (View)
- **LoginView.xaml**: Provides the user interface for login with a password field and login button
- **Events**: 
  - Button click triggers login
  - Enter key in the password field triggers login

### 2. ViewModel Layer
- **LoginViewModel.cs**: Handles the login logic
  - **Properties**:
    - `Password`: Binds to the password input field
    - `ErrorMessage`: Displays authentication errors
    - `IsLoading`: Indicates if authentication is in progress
  - **Commands**:
    - `LoginCommand`: Executes the login process
    - `CancelCommand`: Clears the login form

### 3. Service Layer
- **IUserService.cs**: Interface defining authentication methods
- **MockUserService.cs**: Implementation that provides mock authentication
  - `AuthenticateWithPasswordOnlyAsync()`: Validates the password ("123")
  - Returns User object on success, null on failure

### 4. Navigation
- **NavigationService.cs**: Handles UI navigation after successful login
  - Creates and shows the appropriate view after login
  - Sets DataContext for the new view

### 5. Authentication Flow
1. User enters password and clicks login button or presses Enter
2. `LoginCommand` executes if password is not empty and not already loading
3. `LoginAsync()` method is called:
   - Clears any previous error messages
   - Sets `IsLoading` to true
   - Calls `AuthenticateWithPasswordOnlyAsync()` with the password
4. MockUserService checks if password equals "123"
5. If authentication succeeds:
   - LoginSuccessful event is raised
   - NavigationService navigates to the main application view
6. If authentication fails:
   - Error message is displayed
   - `IsLoading` is set back to false
7. Exception handling occurs at every step with appropriate error messages

## Security Considerations

- This implementation uses a simple mock login with a hard-coded password ("123") for development purposes
- In production, this should be replaced with proper authentication services
- Password should be securely hashed, not stored or compared in plain text
- Multiple failed login attempts should be tracked and limited

## Integration Points

- The login system integrates with:
  - User management system (currently mocked)
  - Navigation service for application flow
  - UI framework for visual feedback
