namespace eoTouchDelivery.Core.Commands
{
    /// <summary>
    /// Common Navigation commands
    /// </summary>
    public static class NavigationCommands
    {
        /// <summary>
        /// Field to hold back nav command
        /// </summary>
        static NavigateBackCommand _navBackCommand;

        /// <summary>
        /// A command which performs a NavigationService.GoBack
        /// </summary>
        public static NavigateBackCommand GoBack => _navBackCommand ?? (_navBackCommand = new NavigateBackCommand ());

        /// <summary>
        /// Field to hold fwd nav command
        /// </summary>
        static NavigateToCommand _navToCommand;

        /// <summary>
        /// A command which performs a NavigationService.Navigate
        /// </summary>
        public static NavigateToCommand NavigateTo => _navToCommand ?? (_navToCommand = new NavigateToCommand ());
    }
}

