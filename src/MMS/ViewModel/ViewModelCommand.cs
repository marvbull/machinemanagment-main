using System;
using System.Windows.Input;

namespace MMS.ViewModel
{
    public class ViewModelCommand : ICommand
    {
        private readonly Action<object?> _executeAction;
        private readonly Predicate<object?>? _canExecuteAction;

        public ViewModelCommand(Action<object?> executeAction, Predicate<object?>? canExecuteAction = null)
        {
            _executeAction = executeAction ?? throw new ArgumentNullException(nameof(executeAction));
            _canExecuteAction = canExecuteAction;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecuteAction == null || _canExecuteAction(parameter);
        }

        public void Execute(object? parameter)
        {
            _executeAction(parameter);
        }
    }
}
