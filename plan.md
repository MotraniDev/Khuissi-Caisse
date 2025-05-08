# Project Development Plan - نظام إدارة الصندوق (AI Agent Iteration)

This plan outlines the development strategy for the AI agent, focusing on a UI/UX-first approach with progressive feature implementation. The core principle is to build functional UI components with mocked backend services initially, then integrate with actual services and data persistence layers. Arabic language support and RTL layout are integral from the outset.

## Overall Strategy

1.  **UI/UX First with Mocking:** Develop user interface components with full Arabic localization and RTL support. Business logic will be initially connected to mocked services to allow for rapid UI iteration and testing.
2.  **Progressive Business Logic Integration:** Implement and integrate real business services one by one, replacing the mocked counterparts. This will be based on the existing service interfaces and implementations outlined in `Project-Todo.md`.
3.  **Data Persistence:** Ensure data models and repositories are fully implemented and connected to the business services. Much of this groundwork is already laid out or completed in `Project-Todo.md`.
4.  **Iterative Development and Testing:** Each feature or component implemented will be followed by a build, run, and test cycle. Errors will be addressed immediately.
5.  **Documentation and Version Control:** `CHANGELOG.md` and `Project-Todo.md` will be updated after each significant change. All changes will be committed to local and remote repositories regularly using GitHub MCP server. Context7 MCP server will be used for documentation needs.

## Development Phases

### Phase 1: Foundational Setup & Core UI Shell (Arabic Focus)

**Goal:** Establish the basic application structure with complete Arabic localization and implement the main shell and login screen.

1.  **Arabic Localization & RTL:**
    - Set up Arabic resource files (`*.resx`).
    - Configure default `FlowDirection` to `RightToLeft` application-wide.
    - Set up appropriate fonts for Arabic text.
    - Ensure culture uses Western digits (0-9) (verify existing `[x]` task).
2.  **Application Shell & Login:**
    - Design and implement the main application shell with RTL support.
    - Create the login view (Arabic UI).
    - Implement basic navigation mechanisms within the shell.
3.  **Mocked Authentication:**
    - Create a `MockUserService` implementing `IUserService` to simulate login/logout functionality for UI development.

### Phase 2: Member Management UI & Mocked Logic

**Goal:** Implement the complete UI for member management, backed by a mocked service.

1.  **Member Views (Arabic & RTL):**
    - Member list view: Arabic search, RTL layout, Arabic date formatting (Gregorian calendar, Western digits).
    - Member details view (Arabic).
    - Member edit view (Arabic input, validation messages in Arabic).
2.  **Mocked Member Service:**
    - Create a `MockMemberService` implementing `IMemberService` to provide sample data and simulate CRUD operations for the member views.

### Phase 3: Subscription & Expense UI & Mocked Logic

**Goal:** Implement UI for subscription and expense management, backed by mocked services.

1.  **Subscription Views (Arabic & RTL):**
    - Record payment view (Arabic currency support with Western digits).
    - Subscription status view.
    - Advance payment view.
2.  **Expense Views (Arabic & RTL):**
    - Expense entry view (Arabic currency support with Western digits).
    - Expense list view with RTL support.
3.  **Mocked Financial Services:**
    - Create a `MockSubscriptionService` implementing `ISubscriptionService`.
    - Create a `MockExpenseService` implementing `IExpenseService`.

### Phase 4: Service Implementation & Integration

**Goal:** Implement the actual business logic services and integrate them with the UI, replacing the mocked versions. Connect to the data persistence layer.

1.  **Complete Entity Models & Repositories:**
    - Verify and complete any pending tasks for `Member`, `Subscription`, `Expense`, `User`, `ApplicationSettings` entity models (referencing `Project-Todo.md` "Phase 2: Core Data Models & Repositories").
    - Ensure all specific repositories (`MemberRepository`, `SubscriptionRepository`, etc.) are fully implemented and functional.
2.  **Implement Core Business Services:**
    - Implement `UserService` (authentication, role management, password management) if not already complete.
    - Refine/Complete `MemberService` using the actual `MemberRepository`.
    - Refine/Complete `SubscriptionService` using the actual `SubscriptionRepository`.
    - Refine/Complete `ExpenseService` using the actual `ExpenseRepository`.
3.  **UI Integration:**
    - Replace mocked services in ViewModels with actual service implementations via Dependency Injection.
    - Thoroughly test UI interaction with live data and services.

### Phase 5: Reporting, Configuration & Advanced Features

**Goal:** Develop reporting capabilities, application settings, and other advanced features.

1.  **Reporting System:**
    - Implement `ReportService`.
    - Create Arabic report templates.
    - Develop RTL-aware report preview and PDF export (Western digits).
2.  **Configuration & Settings:**
    - Implement XML configuration system (if not yet complete).
    - Create application settings view (default subscription amount, backup settings, user management).
3.  **Advanced Features:**
    - Implement backup system (Google Drive, USB, scheduling).
    - Create update checking system.

### Phase 6: Testing, Documentation & Finalization

**Goal:** Ensure application quality, complete documentation, and prepare for deployment.

1.  **Comprehensive Testing:**
    - Write unit tests for services and repositories.
    - Create integration tests for data access.
    - Perform thorough UI testing with real Arabic data.
    - Conduct performance optimization (Arabic text rendering, RTL layout).
2.  **Documentation:**
    - Document code with bilingual XML comments.
    - Create Mermaid flowcharts for key processes (in Arabic) as per `Project-Todo.md`.
    - Create Arabic user documentation.
3.  **Finalization:**
    - Conduct security review.
    - Prepare deployment package.
    - Set up continuous integration/deployment (CI/CD).

This phased approach allows for visible progress in the UI early on, facilitating feedback and iterative refinement, while systematically building out the backend functionality.
