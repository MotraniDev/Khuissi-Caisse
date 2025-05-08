# Project Tasks - نظام إدارة الصندوق (AI Agent Iteration)

This file lists the tasks for the AI agent, prioritized according to the UI-first development plan. Tasks are primarily derived from `Project-Todo.md`. The AI agent should update `Project-Todo.md` by marking tasks `[x]` upon completion.

## Phase 1: Foundational Setup & Core UI Shell (Arabic Focus)

- **Project Setup & Infrastructure (from Project-Todo.md - Phase 1):**
  - [ ] Set up Arabic localization and RTL support
    - [ ] Create Arabic resource files (`*.resx`) for common UI strings (e.g., Login, Username, Password, Cancel, OK).
    - [ ] Configure default `FlowDirection` to `RightToLeft` in `App.xaml` or base window/page styles.
    - [ ] Set up appropriate fonts for Arabic text (e.g., "Traditional Arabic", "Arabic Typesetting"). Ensure they are embedded or commonly available.
    - [ ] Verify: Configure culture to use Western digits (0-9) (already marked `[x]` in `Project-Todo.md`, but confirm effect).
  - [ ] Create XML configuration system (basic structure for app settings if not already robust).
- **UI Development (from Project-Todo.md - Phase 4):**
  - [ ] Design and implement main application shell with RTL support.
    - Create a `MainWindow.xaml` or equivalent shell.
    - Include areas for navigation and content display.
    - Ensure all shell elements respect RTL flow.
  - [ ] Create login view (Arabic).
    - Design `LoginView.xaml` with fields for Username, Password, and a Login button.
    - All text literals must be in Arabic (sourced from `.resx` files).
    - Layout must be RTL.
- **Mock Services:**
  - [ ] Create `MockUserService` implementing `IUserService`.
    - Include methods like `AuthenticateAsync(string username, string password)` returning a mock `User` object or boolean.
    - This mock will be used by the `LoginViewModel`.
- **ViewModel for Login:**
  - [ ] Create `LoginViewModel`.
    - Implement properties for Username, Password.
    - Implement `LoginCommand`.
    - Inject `IUserService` (initially the `MockUserService`).

## Phase 2: Member Management UI & Mocked Logic

- **UI Development (from Project-Todo.md - Phase 4):**
  - [ ] Implement member management views:
    - [ ] Member list view with Arabic search support
      - [ ] Create `MemberListViewModel` and `MemberListView.xaml`.
      - [ ] Display a list of members (initially from mock service).
      - [ ] Implement a search box supporting Arabic text input.
      - [ ] Ensure RTL layout for all components (DataGrid, buttons, search box).
      - [ ] Format dates using Gregorian calendar and Western digits, with Arabic month/day names if appropriate.
    - [ ] Member details view in Arabic
      - [ ] Create `MemberDetailsViewModel` and `MemberDetailsView.xaml`.
      - [ ] Display detailed information for a selected member.
    - [ ] Member edit view with Arabic validation
      - [ ] Create `MemberEditViewModel` and `MemberEditView.xaml` (for adding/editing members).
      - [ ] Include input fields for Name, surname, birth date, phone, address, photo.
      - [ ] Implement validation rules with messages in Arabic (from `.resx` files).
- **Mock Services:**
  - [ ] Create `MockMemberService` implementing `IMemberService`.
    - Provide methods for `GetAllAsync`, `GetByIdAsync`, `AddAsync`, `UpdateAsync`, `DeleteAsync` returning/manipulating a static list of mock `Member` objects.
    - Include mock family relationship data if views require it.
- **ViewModels for Member Management:**
  - [ ] Enhance `MemberListViewModel` to use `MockMemberService`.
    - Implement commands for Add, Edit, Delete members.
    - Implement search functionality.
  - [ ] Implement `MemberDetailsViewModel` to display data from `MockMemberService`.
  - [ ] Implement `MemberEditViewModel` to add/edit members using `MockMemberService`.

## Phase 3: Subscription & Expense UI & Mocked Logic

- **UI Development (from Project-Todo.md - Phase 4):**
  - [ ] Develop subscription management views:
    - [ ] Record payment view with Arabic currency support (Western digits).
    - [ ] Subscription status view.
    - [ ] Advance payment view.
  - [ ] Create expense tracking views:
    - [ ] Expense entry view with Arabic currency (Western digits).
    - [ ] Expense list view with RTL support.
- **Mock Services:**
  - [ ] Create `MockSubscriptionService` implementing `ISubscriptionService`.
    - Mock methods for recording payments, getting status, handling advance payments.
  - [ ] Create `MockExpenseService` implementing `IExpenseService`.
    - Mock methods for recording expenses and listing them.
- **ViewModels for Financial Management:**
  - [ ] Create ViewModels for each of the subscription and expense views, using their respective mock services.

## Phase 4: Service Implementation & Integration

- **Core Data Models & Repositories (from Project-Todo.md - Phase 2 - Verify/Complete):**
  - [ ] (Verify/Complete) Implement Member entity model (Name, surname, birth date, phone, address, photo, Family relationship linking capability).
  - [ ] (Verify/Complete) Implement Subscription entity model (Monthly payment tracking, Default amount, Advance payment support).
  - [ ] (Verify/Complete) Implement Expense entity model (Amount, beneficiary, purpose).
  - [ ] (Verify/Complete) Implement User entity model (Username, password hash, role).
  - [ ] (Verify/Complete) Implement ApplicationSettings entity model (Default subscription amount, backup settings).
  - [ ] (Verify/Complete) Create base Repository interface and implementation (`IRepository<T>`, `Repository<T>`).
  - [ ] (Verify/Complete) Implement specific repositories:
    - [ ] (Verify/Complete) `MemberRepository`.
    - [ ] (Verify/Complete) `SubscriptionRepository`.
    - [ ] (Verify/Complete) `ExpenseRepository`.
    - [ ] (Verify/Complete) `UserRepository`.
    - [ ] (Verify/Complete) `ApplicationSettingsRepository`.
- **Business Services (from Project-Todo.md - Phase 3 - Implement/Refine):**
  - [ ] (Implement/Refine) `UserService` for authentication (User authentication, Role-based authorization, Password management). Connect to `UserRepository`.
  - [ ] (Refine) `MemberService` for member management (CRUD, Search, Family relationship management). Connect to `MemberRepository`.
  - [ ] (Refine) `SubscriptionService` (Record monthly subscriptions, Handle advance payments, Calculate subscription status). Connect to `SubscriptionRepository`.
  - [ ] (Refine) `ExpenseService` (Record fund expenses, Categorize expenses). Connect to `ExpenseRepository`.
- **Integration:**
  - [ ] Update Dependency Injection configuration to provide real services instead of mocks.
  - [ ] Test all UI views with real services and database interaction.
  - [ ] Resolve any issues arising from integration.
- **Authentication System (from Project-Todo.md - Phase 1):**
  - [ ] Design and implement authentication system (this will now use the real `UserService`).

## Phase 5: Reporting, Configuration & Advanced Features

- **Business Services (from Project-Todo.md - Phase 3):**
  - [ ] Create `ReportService`
    - [ ] Weekly/monthly/yearly fund status.
    - [ ] Individual subscription status.
    - [ ] PDF generation capability.
- **UI Development (from Project-Todo.md - Phase 4):**
  - [ ] Implement reporting views:
    - [ ] Arabic report templates.
    - [ ] RTL-aware report preview.
    - [ ] Arabic PDF export with Western digits.
- **Advanced Features (from Project-Todo.md - Phase 5):**
  - [ ] Implement backup system
    - [ ] Google Drive integration.
    - [ ] USB backup functionality.
    - [ ] Backup scheduling.
  - [ ] Create update checking system
    - [ ] JSON version file parsing.
    - [ ] Download and update mechanism.
  - [ ] Implement application settings view
    - [ ] Default subscription amount configuration.
    - [ ] Backup settings.
    - [ ] User management.

## Phase 6: Testing, Documentation & Finalization

- **Testing & Documentation (from Project-Todo.md - Phase 6):**
  - [ ] Write unit tests for services and repositories.
  - [ ] Create integration tests for data access.
  - [ ] Document code with bilingual XML comments.
  - [ ] Create Mermaid flowcharts for key processes (in Arabic):
    - [ ] تسجيل الأعضاء (Member registration workflow)
    - [ ] عملية دفع الاشتراك (Subscription payment process)
    - [ ] تسجيل المصاريف (Expense recording process)
    - [ ] إنشاء التقارير (Report generation workflow)
    - [ ] عملية النسخ الاحتياطي (Backup process)
    - [ ] عملية التحديث (Update process)
- **Finalization (from Project-Todo.md - Phase 7):**
  - [ ] Perform application performance optimization
    - [ ] Arabic text rendering optimization.
    - [ ] RTL layout performance testing.
  - [ ] Conduct security review.
  - [ ] Test application with real Arabic data.
  - [ ] Create Arabic user documentation.
  - [ ] Prepare deployment package.
  - [ ] Set up continuous integration/deployment.

**Note:** Tasks marked `(Verify/Complete)` or `(Implement/Refine)` acknowledge that some work may already be done as per `Project-Todo.md`. The AI agent should check the existing codebase and `Project-Todo.md` status for these items.
