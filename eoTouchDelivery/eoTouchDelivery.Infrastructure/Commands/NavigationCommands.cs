namespace eoTouchDelivery.Infrastructure.Commands
{
    /// <summary>
    /// Commands which perform common navigation 
    /// </summary>
    public static class NavigationCommands
    {
        /// <summary>
        /// Field to hold back nav command
        /// </summary>
        static NavigateBackCommand navBackCommand;

        /// <summary>
        /// A command which performs a NavigationService.GoBack
        /// </summary>
        public static NavigateBackCommand GoBack => (navBackCommand != null)
                    ? navBackCommand
                    : (navBackCommand = new NavigateBackCommand ());

        /// <summary>
        /// Field to hold fwd nav command
        /// </summary>
        static NavigateToCommand navToCommand;

        /// <summary>
        /// A command which performs a NavigationService.Navigate
        /// </summary>
        public static NavigateToCommand NavigateTo => (navToCommand != null)
                    ? navToCommand
                    : (navToCommand = new NavigateToCommand ());
    }
}

