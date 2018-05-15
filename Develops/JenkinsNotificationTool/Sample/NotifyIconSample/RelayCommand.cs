namespace NotifyIconSample
{
    using System;
    using System.Windows.Input;
    using NotifyIconSample.Annotations;

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;

        private readonly Func<object, bool> _canExecute;

        public RelayCommand([NotNull] Action<object> execute)
        {
            if (execute == null) throw new ArgumentNullException(nameof(execute));
            _execute = execute;
        }

        public RelayCommand([NotNull] Action<object> execute, [NotNull] Func<object, bool> canExecute)
        {
            if (execute == null) throw new ArgumentNullException(nameof(execute));
            if (canExecute == null) throw new ArgumentNullException(nameof(canExecute));

            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
            CommandManager.InvalidateRequerySuggested();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}