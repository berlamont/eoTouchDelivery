using System;
using System.Windows.Input;
using eoTouchDelivery.Core.Interfaces;
using eoTouchDelivery.Core.Services;

namespace eoTouchDelivery.Core.Commands
{
    /// <summary>
    /// This command uses the registered INavigationService to perform
    /// a backwards navigation.
    /// </summary>
    public class NavigateBackCommand : ICommand
    {
        bool monitorNavigationStack;

        /// <summary>
        /// Protected ctor - only allow library to create command
        /// unless you derive from it. Should alway use NavigationCommands.
        /// </summary>
        protected internal NavigateBackCommand ()
        {
        }

        /// <summary>
        /// True/False to monitor the enavigation and raise our CanExecuteChanged
        /// in response.
        /// </summary>
        public bool MonitorNavigationStack
        {
            get
            {
                return monitorNavigationStack;
            }

            set
            {
                monitorNavigationStack = value;

                var ns = DependencyService.ServiceLocator.Get<INavigationService> ();
                if (ns != null) {
                    // Always unsubscribe to ensure we are never > 1.
                    ns.Navigated -= OnUpdateCanExecuteChanged;
                    ns.NavigatedBack -= OnUpdateCanExecuteChanged;

                    if (monitorNavigationStack) {
                        ns.Navigated += OnUpdateCanExecuteChanged;
                        ns.NavigatedBack += OnUpdateCanExecuteChanged;
                    }
                }
            }
        }

        /// <summary>
        /// Event raised when the state of the NavigateBackCommand has changed.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// This is called when the navigation stack has changed.
        /// It refreshes the state of the command.
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="e">Empty EventArgs</param>
        void OnUpdateCanExecuteChanged (object sender, EventArgs e)
        {
            CanExecuteChanged?.Invoke (this, EventArgs.Empty);
        }

        /// <summary>
        /// This is called to determine whether the command can be executed.
        /// We use the current navigation stack state.
        /// </summary>
        /// <returns>True if the command is valid</returns>
        /// <param name="parameter">Parameter.</param>
        public bool CanExecute (object parameter)
        {
            var ns = DependencyService.ServiceLocator.Get<INavigationService> ();
            return ns != null && ns.CanGoBack;
        }

        /// <summary>
        /// This is called to execute the command.
        /// </summary>
        /// <param name="parameter">Not used</param>
        public async void Execute (object parameter)
        {
            var ns = DependencyService.ServiceLocator.Get<INavigationService> ();
            if (ns != null) {
                await ns.GoBackAsync ();
            }
        }
    }
}

