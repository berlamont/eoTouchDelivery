using System;
using System.Windows.Input;
using eoTouchDelivery.Core.Services;
using eoTouchDelivery.Core.Interfaces;

namespace eoTouchDelivery.Core.Commands
{
    /// <summary>
    /// Command to perform a NavigateAsync to a specific page
    /// </summary>
    public class NavigateToCommand : ICommand
    {
        /// <summary>
        /// Only allow library to create command unless you derive from it. Should use NavigationCommands otherwise.
        /// </summary>
        protected internal NavigateToCommand ()
        {
        }

        /// <summary>
        /// Raised when state of NavigateBackCommand has changed.
        /// </summary>
#pragma warning disable 67
        public event EventHandler CanExecuteChanged;
#pragma warning restore 67

        /// <summary>
        /// Check if command can be executed.
        /// </summary>
        /// <returns>True if the command is valid</returns>
        /// <param name="parameter">PageKey to navigate to</param>
        public bool CanExecute (object parameter) => parameter != null;

        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="parameter">Page Key to navigate to</param>
        public async void Execute (object parameter)
        {
            if (parameter == null) return;
            var ns = DependencyService.ServiceLocator.Get<INavigationService> ();
            if (ns != null)
                await ns.NavigateAsync (parameter);
        }
    }
}

