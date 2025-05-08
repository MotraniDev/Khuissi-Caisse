using System;
using System.Windows.Input;

namespace Khouissi_Caisse.ViewModels;

/// <summary>
/// Implementation of ICommand that delegates to methods or lambda expressions
/// </summary>
public class RelayCommand : ICommand
{
    private readonly Action<object?> _execute;
    private readonly Func<object?, bool>? _canExecute;

    /// <summary>
    /// Event raised when the ability to execute the command changes
    /// </summary>
    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    /// <summary>
    /// Creates a new RelayCommand instance
    /// </summary>
    /// <param name="execute">Action to execute when command is invoked</param>
    /// <param name="canExecute">Function that determines if command can be executed (optional)</param>
    public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    /// <summary>
    /// Determines if the command can be executed
    /// </summary>
    /// <param name="parameter">Parameter for the command</param>
    /// <returns>True if command can be executed, false otherwise</returns>
    public bool CanExecute(object? parameter)
    {
        return _canExecute == null || _canExecute(parameter);
    }

    /// <summary>
    /// Executes the command
    /// </summary>
    /// <param name="parameter">Parameter for the command</param>
    public void Execute(object? parameter)
    {
        _execute(parameter);
    }

    /// <summary>
    /// Raises the CanExecuteChanged event
    /// </summary>
    public void RaiseCanExecuteChanged()
    {
        CommandManager.InvalidateRequerySuggested();
    }
}

/// <summary>
/// Generic version of RelayCommand that provides strong typing for the command parameter
/// </summary>
/// <typeparam name="T">Type of the command parameter</typeparam>
public class RelayCommand<T> : ICommand
{
    private readonly Action<T?> _execute;
    private readonly Predicate<T?>? _canExecute;

    /// <summary>
    /// Event raised when the ability to execute the command changes
    /// </summary>
    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    /// <summary>
    /// Creates a new RelayCommand instance
    /// </summary>
    /// <param name="execute">Action to execute when command is invoked</param>
    /// <param name="canExecute">Function that determines if command can be executed (optional)</param>
    public RelayCommand(Action<T?> execute, Predicate<T?>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    /// <summary>
    /// Determines if the command can be executed
    /// </summary>
    /// <param name="parameter">Parameter for the command</param>
    /// <returns>True if command can be executed, false otherwise</returns>
    public bool CanExecute(object? parameter)
    {
        return _canExecute == null || _canExecute(parameter is T t ? t : default);
    }

    /// <summary>
    /// Executes the command
    /// </summary>
    /// <param name="parameter">Parameter for the command</param>
    public void Execute(object? parameter)
    {
        _execute(parameter is T t ? t : default);
    }

    /// <summary>
    /// Raises the CanExecuteChanged event
    /// </summary>
    public void RaiseCanExecuteChanged()
    {
        CommandManager.InvalidateRequerySuggested();
    }
}