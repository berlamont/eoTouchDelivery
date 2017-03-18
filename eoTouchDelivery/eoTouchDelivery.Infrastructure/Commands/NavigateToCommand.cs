using System;
using System.Windows.Input;
using eoTouchDelivery.Interfaces;
using eoTouchDelivery.Services;

namespace eoTouchDelivery.Commands
{
    /// <summary>
    /// This class implements an ICommand which will use the registered INavigationService
    /// to perform a NavigateAsync to a specific page
    /// </summary>
    public class NavigateToCommand : ICommand
    {
        /// <summary>
        /// Protected ctor - only allow library to create command
        /// unless you derive from it. Should alway use NavigationCommands.
        /// </summary>
        protected internal NavigateToCommand ()
        {
        }

        /// <summary>
        /// Event raised when the state of the NavigateBackCommand has changed.
        /// </summary>
#pragma warning disable 67
        public event EventHandler CanExecuteChanged;
#pragma warning restore 67

        /// <summary>
        /// This is called to determine whether the command can be executed.
        /// </summary>
        /// <returns>True if the command is valid</returns>
        /// <param name="parameter">PageKey to navigate to</param>
        public bool CanExecute (object parameter)
        {
            // Must have a page key.
            return parameter != null;
        }

        /// <summary>
        /// This is called to execute the command.
        /// </summary>
        /// <param name="parameter">Page Key to navigate to</param>
        public async void Execute (object parameter)
        {
            if (parameter != null)
            {
                var ns = DependencyService.ServiceLocator.Get<INavigationService> ();
                if (ns != null) {
                    await ns.NavigateAsync (parameter);
                }
            }
        }
    }
}

