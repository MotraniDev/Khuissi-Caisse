# Project Todo List - نظام إدارة الصندوق

This file tracks the progress of development tasks for the نظام إدارة الصندوق (Subscription Management Fund) application.

## Phase 1: Project Setup & Infrastructure

- [x] Set up Arabic localization and RTL support
  - [x] Create Arabic resource files (`*.resx`) for common UI strings (e.g., Login, Username, Password, Cancel, OK).
  - [x] Configure default `FlowDirection` to `RightToLeft` in `App.xaml` or base window/page styles.
  - [x] Set up appropriate fonts for Arabic text (e.g., "Traditional Arabic", "Arabic Typesetting").
  - [x] Configure culture to use Western digits (0-9).
- [ ] Create XML configuration system
  - [ ] Create a basic configuration file structure (XML-based).
  - [ ] Implement a service for reading/writing configuration.
  - [ ] Define settings schema (application settings, user preferences).
- [x] Design and implement authentication system
  - [x] Create user service interface.
  - [x] Implement mock user service for UI development.
  - [x] Set up login view with Arabic UI.
  - [x] Implement password security handling.

## Phase 2: Core Data Models & Repositories

- [x] Implement Member entity model
  - [x] Basic properties (Name, surname, birth date, phone, address, photo).
  - [x] Family relationship linking capability.
- [x] Implement Subscription entity model
  - [x] Monthly payment tracking.
  - [x] Default amount.
  - [x] Advance payment support.
- [x] Implement Expense entity model
  - [x] Amount, beneficiary, purpose.
- [x] Implement User entity model
  - [x] Username, password hash, role.
- [x] Implement ApplicationSettings entity model
  - [x] Default subscription amount.
  - [x] Backup settings.
- [x] Create base Repository interface and implementation
  - [x] Generic `IRepository<T>` interface.
  - [x] Base `Repository<T>` implementation.
- [x] Implement specific repositories
  - [x] `MemberRepository`.
  - [x] `SubscriptionRepository`.
  - [x] `ExpenseRepository`.
  - [x] `UserRepository`.
  - [x] `ApplicationSettingsRepository`.

## Phase 3: Business Services

- [x] Create UserService for authentication
  - [x] User authentication.
  - [x] Role-based authorization.
  - [x] Password management.
- [x] Create MemberService for member management
  - [x] Define interface with core operations.
  - [x] Implement mock service for UI development.
  - [ ] Implement full CRUD operations for members.
  - [ ] Add search functionality.
  - [ ] Implement family relationship management.
- [ ] Create SubscriptionService for subscription management
  - [ ] Record monthly subscriptions.
  - [ ] Handle advance payments.
  - [ ] Calculate subscription status.
- [ ] Create ExpenseService for expense tracking
  - [ ] Record fund expenses.
  - [ ] Categorize expenses.
- [ ] Create ReportService for generating reports
  - [ ] Weekly/monthly/yearly fund status.
  - [ ] Individual subscription status.
  - [ ] PDF generation capability.

## Phase 4: UI Development

- [x] Design and implement main application shell with RTL support
  - [x] Create a `MainWindow.xaml` or equivalent shell.
  - [x] Include areas for navigation and content display.
  - [x] Ensure all shell elements respect RTL flow.
- [x] Create login view (Arabic)
  - [x] Design `LoginView.xaml` with fields for Username, Password, and a Login button.
  - [x] All text literals in Arabic (sourced from `.resx` files).
  - [x] Layout in RTL.
- [ ] Implement member management views
  - [x] Initial Member list view setup.
  - [ ] Complete Member list view with Arabic search support.
  - [ ] Member details view in Arabic.
  - [ ] Member edit view with Arabic validation.
- [ ] Develop subscription management views
  - [ ] Record payment view with Arabic currency support (Western digits).
  - [ ] Subscription status view.
  - [ ] Advance payment view.
- [ ] Create expense tracking views
  - [ ] Expense entry view with Arabic currency (Western digits).
  - [ ] Expense list view with RTL support.
- [ ] Implement reporting views
  - [ ] Arabic report templates.
  - [ ] RTL-aware report preview.
  - [ ] Arabic PDF export with Western digits.

## Phase 5: Advanced Features

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

## Phase 6: Testing & Documentation

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

## Phase 7: Finalization

- [ ] Perform application performance optimization
  - [ ] Arabic text rendering optimization.
  - [ ] RTL layout performance testing.
- [ ] Conduct security review.
- [ ] Test application with real Arabic data.
- [ ] Create Arabic user documentation.
- [ ] Prepare deployment package.
- [ ] Set up continuous integration/deployment.
