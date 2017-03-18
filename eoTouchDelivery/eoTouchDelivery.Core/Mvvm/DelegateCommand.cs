using System;

namespace eoTouchDelivery.Infrastructure
{
    /// <summary>
    /// Implementation of ICommand using delegates.
    /// This is preferred over Command in Forms so it can be mocked/replaced
    /// in the ViewModel and have your VM not take a dependency on Forms.
    /// </summary>
	public sealed class DelegateCommand : IDelegateCommand
	{
        /// <summary>
        /// Delegate to call when the Execute method is called.
        /// </summary>
		readonly Action<object> command;

        /// <summary>
        /// Delegate to call when the CanExecute method is called.
        /// </summary>
		readonly Func<object, bool> canExecute;

        /// <summary>
        /// Event to raise when the state of the command has changed.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Creates a new DelegateCommand.
        /// </summary>
        /// <param name="command">Delegate to call for command</param>
		public DelegateCommand (Action<object> command) : this (command, null)
		{
		}

        /// <summary>
        /// Creates a new DelegateCommand.
        /// </summary>
        /// <param name="command">Delegate to call for command</param>
		public DelegateCommand (Action command) : this (command, null)
		{
		}

        /// <summary>
        /// Creates a new DelegateCommand.
        /// </summary>
        /// <param name="command">Delegate to call for command</param>
        /// <param name="test">Delegate to call for CanExecute</param>
		public DelegateCommand (Action command, Func<bool> test)
		{
			if (command == null)
				throw new ArgumentNullException ("command", "Command cannot be null.");
			this.command = delegate {
				command ();
			};
			if (test != null) {
				this.canExecute = delegate {
					return test ();
				};
			}
		}

        /// <summary>
        /// Creates a new DelegateCommand.
        /// </summary>
        /// <param name="command">Delegate to call for command</param>
        /// <param name="test">Delegate to call for CanExecute</param>
		public DelegateCommand (Action<object> command, Func<object, bool> test)
		{
			if (command == null)
				throw new ArgumentNullException ("command", "Command cannot be null.");

			this.command = command;
			this.canExecute = test;
		}

        /// <summary>
        /// Raises the CanExecuteChanged event.
        /// </summary>
		public void RaiseCanExecuteChanged ()
		{
			this.CanExecuteChanged?.Invoke (this, EventArgs.Empty);
		}

        /// <summary>
        /// Checks to see if the command is valid.
        /// </summary>
        /// <returns><c>true</c>, if execute was caned, <c>false</c> otherwise.</returns>
        /// <param name="parameter">Parameter.</param>
		public bool CanExecute (object parameter)
		{
			return (this.canExecute == null) || this.canExecute (parameter);
		}

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameter">Parameter.</param>
		public void Execute (object parameter)
		{
			this.command (parameter);
		}
	}

    /// <summary>
    /// Generic form of the DelegateCommand with a parameter.
    /// </summary>
	public sealed class DelegateCommand<T> : IDelegateCommand
	{
        /// <summary>
        /// Delegate to call when the Execute method is called.
        /// </summary>
        readonly Action<T> command;

        /// <summary>
        /// Delegate to call when the CanExecute method is called.
        /// </summary>
        readonly Func<T, bool> canExecute;

        /// <summary>
        /// Event to raise when the state of the command has changed.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Creates a new Delegate command
        /// </summary>
        /// <param name="command">Delegate to invoke</param>
		public DelegateCommand (Action<T> command) : this (command, null)
		{
		}

        /// <summary>
        /// Creates a new Delegate command
        /// </summary>
        /// <param name="command">Delegate to invoke</param>
        /// <param name="test">Delegate for CanExecute</param>
		public DelegateCommand (Action<T> command, Func<T, bool> test)
		{
			if (command == null)
				throw new ArgumentNullException ("command", "Command cannot be null.");

			this.command = command;
			this.canExecute = test;
		}

        /// <summary>
        /// Raises the CanExecuteChanged event.
        /// </summary>
		public void RaiseCanExecuteChanged ()
		{
			this.CanExecuteChanged?.Invoke (this, EventArgs.Empty);
		}

        /// <summary>
        /// Returns whether the command is valid.
        /// </summary>
        /// <returns><c>true</c>, if execute was caned, <c>false</c> otherwise.</returns>
        /// <param name="parameter">Parameter.</param>
		public bool CanExecute (object parameter)
		{
			return (this.canExecute == null) || this.canExecute ((T)parameter);
		}

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameter">Parameter.</param>
		public void Execute (object parameter)
		{
			this.command ((T)parameter);
		}
	}
}

