using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ComixShopWPF.Commands.@base;

namespace ComixShopWPF.Commands.@base
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool>? _canExecute;

        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        // Событие для оповещения об изменении возможности выполнения команды
        public event EventHandler? CanExecuteChanged;

        // Определяет, может ли команда выполняться
        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;

        // Выполняет команду
        public void Execute(object? parameter) => _execute(parameter);

        // Метод для вызова события CanExecuteChanged
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
