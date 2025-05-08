# AI Agent Instructions for "نظام إدارة الصندوق" Project

These instructions guide your development process for the "نظام إدارة الصندوق" (Subscription Management Fund) application. Adherence to these instructions is critical for successful project completion.

## Core Development Workflow

1.  **Understand the Task:** Before starting, thoroughly review the assigned task from `tasks.md` and relevant sections in `plan.md` and `Project-Todo.md`.
2.  **Implement Feature/Fix:**
    - Write code following MVVM, C# conventions, and project-specific rules (see `Project-Rules.md`).
    - **Arabic First:** All UI elements must be designed for Arabic language and RTL layout from the start. Use `.resx` files for all display strings.
    - **Mocking (Initial Phases):** For UI development in early phases, create and use Mock Services that implement the required service interfaces. These mocks will provide sample data and simulate behavior, allowing UI to be built independently of the full backend. Example: `MockUserService`, `MockMemberService`.
3.  **Build:** After making code changes, ALWAYS build the solution:
    ```bash
    dotnet build "Khouissi-Caisse.sln"
    ```
4.  **Run & Test:**
    - Run the application:
      ```bash
      dotnet run --project "Khouissi-Caisse/Khouissi-Caisse.csproj"
      ```
    - Manually test the implemented feature or bug fix thoroughly. Pay close attention to UI in Arabic, RTL layout, and data handling (including Western digits for numbers).
5.  **Log & Fix Errors:**
    - Carefully read any output logs (console, Serilog files) and UI error messages.
    - Fix any compilation or runtime errors immediately. Address one error at a time.
    - Re-build and re-test after each fix.
6.  **Update Documentation & Project Tracking:**
    - **`Project-Todo.md`:** Update the status of the completed task(s) in `Project-Todo.md` by changing `[ ]` to `[x]`. Be precise and only mark what is fully done.
    - **`CHANGELOG.md`:** Add a concise entry to `CHANGELOG.md` describing the change (feature, fix, refactor). Follow the existing format.
7.  **Commit Changes (Local & Remote):**
    - Use specific file names in `git add`.
    - Write a clear, conventional commit message.
    - ```bash
      git add [specific files changed, e.g., "Khouissi-Caisse/Views/LoginView.xaml" "Khouissi-Caisse/ViewModels/LoginViewModel.cs"]
      git commit -m "type(scope): descriptive message in English (e.g., feat(login): implement Arabic login UI with mock service)"
      ```
    ````
    *   Push changes to the remote repository frequently (e.g., after each completed sub-task or significant step):
      ```bash
      git push origin <current-branch-name>
      ```
      (Assuming `origin` is the remote name and you are on a feature branch or main as appropriate).
    ````

## Compilation and Error Checking

- ALWAYS check for compilation errors after making code changes using `dotnet build`.
- Fix all compilation errors before proceeding.
- Pay special attention to:
  - Type mismatches.
  - Missing using directives or references.
  - Incorrect access modifiers.
  - Nullable reference type warnings/errors (address them; don't just suppress).
  - XAML parsing errors.

## Code Style and Conventions

- Follow Microsoft C# coding conventions.
- **MVVM:** Strictly adhere to the Model-View-ViewModel pattern.
  - Views (XAML): UI definition, minimal code-behind (event handlers forwarding to ViewModel commands).
  - ViewModels: Presentation logic, state management for the View, expose data via properties (`INotifyPropertyChanged`) and actions via commands (`ICommand`).
  - Models: Represent data entities.
- **Dependency Injection:** Use constructor injection for dependencies (services, repositories) in ViewModels and Services. Configure DI in `App.xaml.cs` or a dedicated DI setup class.
- **Async/Await:** Use `async`/`await` for all I/O-bound operations (database access, network calls) to keep the UI responsive.
- **Localization:**
  - All UI strings **MUST** be stored in `.resx` files (e.g., `Resources.ar-DZ.resx`, `Resources.resx` as fallback).
  - Use `x:Uid` in XAML for localization or bind to ViewModel properties that fetch localized strings.
  - Ensure `FlowDirection="RightToLeft"` is applied correctly.
  - Use culture-aware formatting for dates, numbers, currency (Western digits for numbers/currency as specified).
- **Fonts for Arabic:** Use fonts that provide good Arabic script support (e.g., "Traditional Arabic", "Segoe UI", "Tahoma").

## Git Workflow & GitHub MCP Server Integration

- **ALWAYS use GitHub MCP server tools for repository operations when specified or appropriate (e.g., creating PRs, issues). For basic `add`, `commit`, `push`, use the CLI commands shown above.**
- **Branching:** For significant features, consider creating a feature branch: `git checkout -b feature/feature-name`. Merge back to `main` or `develop` via Pull Requests (created using GitHub MCP server tools if instructed).
- **Commits:**
  - Follow conventional commits format: `type(scope): description`.
  - Types: `feat`, `fix`, `docs`, `style`, `refactor`, `test`, `chore`, `ui`.
  - Scope: Module or area of the app (e.g., `login`, `member`, `subscription`, `rtl`).
  - Example: `feat(member): implement member list view with Arabic search`
- **Remote Repository:**
  - The primary remote repository should be `MotraniDev/Khouissi-Caisse` on GitHub.
  - Push changes regularly. Before ending a session, ensure all committed changes are pushed.
  - Start sessions with `git fetch` and `git pull --rebase origin <current-branch-name>` (or `main`) to stay updated.

## Context7 MCP Server Integration (Documentation)

- **Primary Source:** For ALL .NET, C#, WPF, PowerShell, or other library documentation needs, **FIRST** attempt to use the Context7 MCP server.
- **Workflow:**
  1.  Resolve Library ID: Use `590_resolve-library-id` (e.g., for "wpf", "dotnet", "entityframeworkcore").
  2.  Get Documentation: Use `590_get-library-docs` with the resolved ID and specific `topic` if needed.
- **Code Examples:** Utilize Context7 for code snippets and best practice examples.
- **XML Documentation:** Document all public classes, methods, and properties using standard C# XML documentation comments. Context7 can help verify standards.

## Specific Project Rules (from `Project-Rules.md`)

- **Language:** Arabic is primary for UI. Western digits (0-9) for numeric content.
- **Database:** SQLite with EF Core. `nvarchar` for text. Proper collation for Arabic.
- **Error Handling:** Structured `try-catch`. Log exceptions via Serilog. User-friendly Arabic error messages.
- **Logging:** Use Serilog. Log to file and console.
- **Security:** Password hashing (bcrypt). Input validation.

## Updating `Project-Todo.md`

- After successfully completing a task (implemented, built, run, tested, and committed), locate the corresponding task in `Project-Todo.md`.
- Change the `[ ]` to `[x]` for that task and its sub-tasks if applicable.
- Do NOT modify the text of the task itself unless instructed.
- Commit the updated `Project-Todo.md` along with your code changes or as a separate chore commit.
  ```bash
  git add Project-Todo.md
  git commit -m "chore(todo): mark 'task description' as completed"
  ```

By following these instructions meticulously, you will contribute effectively to the development of the "نظام إدارة الصندوق" application. If any instruction is unclear, seek clarification.
