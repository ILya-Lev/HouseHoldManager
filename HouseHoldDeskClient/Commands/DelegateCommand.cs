using System;
using System.Windows.Input;

namespace HouseHoldDeskClient.Commands
{
	public class DelegateCommand : ICommand
	{
		private readonly Func<object, bool> _canExecute;
		private readonly Action<object> _execute;

		public DelegateCommand(Func<object, bool> canExecute, Action<object> execute)
		{
			if (execute == null)
			{
				throw new ArgumentNullException(nameof(execute),
					$"{nameof(DelegateCommand)} cannot be based on null action");
			}
			_canExecute = canExecute;
			_execute = execute;
		}

		public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

		public void Execute(object parameter) => _execute.Invoke(parameter);

		public event EventHandler CanExecuteChanged;

		public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
	}
}