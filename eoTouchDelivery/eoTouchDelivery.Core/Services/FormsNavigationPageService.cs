using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eoTouchDelivery.Core.Interfaces;
using Xamarin.Forms;

namespace eoTouchDelivery.Core.Services
{
    /// <summary>
    /// Service to manage the Xamarin.Forms Stack navigation system.
    /// This understands both <c>NavigationPage</c> and <c>MasterDetailPage</c> with
    /// an embedded navigation page.
    /// </summary>
    public class FormsNavigationPageService : INavigationService
    {
        static readonly Task TaskCompleted = Task.FromResult(0);
        INavigation _navigation;
        Dictionary<object, Func<Page>> _registeredPages;
        Dictionary<object, Action<object>> _registeredActions;

        /// <summary>
        /// Event raised when NavigateAsync is used.
        /// </summary>
        public event EventHandler Navigated;

        /// <summary>
        /// Event raised when a GoBackAsync operation occurs.
        /// </summary>
        public event EventHandler NavigatedBack;

        /// <summary>
        /// Constructor
        /// </summary>
        public FormsNavigationPageService ()
        {
            // If we are using MasterDetailPage as the MainPage, then
            // hide master page when we navigate on phones since we only look at
            // the detail page for the navigation root.
            HideMasterPageOnNavigation = Device.Idiom == TargetIdiom.Phone;
        }

        /// <summary>
        /// Allows you to change how keys are compared.
        /// Must be called _before_ any pages are registered.
        /// </summary>
        /// <value>The key comparer.</value>
        public IEqualityComparer<object> KeyComparer
        {
            get
            {
                return _registeredPages?.Comparer;
            }

            set
            {
                if (value == null)
                    throw new ArgumentNullException (nameof(KeyComparer), "KeyComparer cannot be null.");
                if (_registeredPages != null)
                    throw new InvalidOperationException ("Cannot set KeyComparer once pages are added.");
                _registeredPages = new Dictionary<object, Func<Page>> (value);
            }
        }

        /// <summary>
        /// This flag determines whether to hide the master page when a NavigateAsync
        /// occurs. The default is TRUE for phones, but you can set this flag to FALSE 
        /// to turn off this behavior.
        /// </summary>
        /// <value><c>true</c> if hide master page on navigation; otherwise, <c>false</c>.</value>
        public bool HideMasterPageOnNavigation
        {
            get; set;
        }

        /// <summary>
        /// Register a page with a known key.
        /// </summary>
        /// <param name="pageKey">Page key.</param>
        /// <param name="creator">Creator.</param>
	    public void RegisterPage(object pageKey, Func<Page> creator)
	    {
            if (pageKey == null)
                throw new ArgumentNullException(nameof(pageKey));
	        if (creator == null)
	            throw new ArgumentNullException(nameof (creator));

            if (_registeredPages == null)
                _registeredPages = new Dictionary<object, Func<Page>> ();
   
            _registeredPages.Add(pageKey, creator);
	    }

        /// <summary>
        /// Registers an action in response to a navigation request.
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="action">Action to perform, gets passed the viewModel parameter.</param>
        public void RegisterAction(object key, Action<object> action)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if (_registeredActions == null)
                _registeredActions = new Dictionary<object, Action<object>>();
            _registeredActions.Add(key, action);
        }

        /// <summary>
        /// Registers an action in response to a navigation request.
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="action">Action to perform</param>
        public void RegisterAction(object key, Action action)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if (_registeredActions == null)
                _registeredActions = new Dictionary<object, Action<object>>();
            _registeredActions.Add(key, unused => action());
        }

        /// <summary>
        /// Unregister a known page/action by key.
        /// </summary>
        /// <param name="key">Page key.</param>
        public void Unregister(object key)
	    {
            if (key == null)
                throw new ArgumentNullException(nameof (key));

            _registeredPages?.Remove(key);
	        _registeredActions?.Remove(key);
	    }

        /// <summary>
        /// Locates a page creator by key.
        /// </summary>
        /// <returns>The page by key.</returns>
        /// <param name="pageKey">Page key.</param>
        Page GetPageByKey(object pageKey)
        {
            if (pageKey == null)
                throw new ArgumentNullException (nameof (pageKey));

            if (_registeredPages == null)
                return null;

            Func<Page> creator;
            return _registeredPages.TryGetValue(pageKey, out creator) ? creator.Invoke() : null;
        }

        /// <summary>
        /// Method used to locate the NavigationPage - looks either on the 
        /// MainPage or, in the case of a MasterDetail setup, on the Details page.
        /// </summary>
        /// <returns>The navigation page.</returns>
        static NavigationPage FindNavigationPage()
        {
            // Most of the time this is good.
            var navPage = Application.Current.MainPage as NavigationPage;
            if (navPage != null) return navPage;
            // Special case for Master/Detail page.
            var mdPage = Application.Current.MainPage as MasterDetailPage;
            if (mdPage != null)
                // Should always have a NavigationPage as the Detail
                navPage = mdPage.Detail as NavigationPage;

            return navPage;
        }

        /// <summary>
        /// Returns the underlying Navigation interface implemented by the
        /// Forms page system.
        /// </summary>
        /// <value>The INavigation implementation to use for navigation.</value>
        public INavigation Navigation
        {
            get
            {
                if (_navigation != null) return _navigation;
                // Locate the navigation page.
                var navPage = FindNavigationPage ();
                if (navPage == null)
                    throw new Exception ("Failed to locate required NavigationPage from App.MainPage.");

                // Cache off Navigation interface.
                _navigation = navPage.Navigation;

                // Wire into navigation events.
                navPage.Pushed += OnPagePushed;
                navPage.Popped += OnPagePopped;
                navPage.PoppedToRoot += OnPagePopped;

                return _navigation;
            }

            set
            {
                _navigation = value;
            }
        }
        
        /// <summary>
        /// Method called when a page is pushed onto the Navigation stack.
        /// </summary>
        /// <param name="sender">NavigationPage</param>
        /// <param name="e">Details</param>
        void OnPagePushed (object sender, Xamarin.Forms.NavigationEventArgs e)
        {
            Navigated?.Invoke (this, EventArgs.Empty);
        }

        /// <summary>
        /// Method called when a page is popped off the Navigation stack,
        /// or when we pop to root.
        /// </summary>
        /// <param name="sender">NavigationPage</param>
        /// <param name="e">Details</param>
        void OnPagePopped (object sender, Xamarin.Forms.NavigationEventArgs e)
        {
            NavigatedBack?.Invoke (this, EventArgs.Empty);
        }

        /// <summary>
        /// Navigate to a page using the passed key. This also assigns the
        /// BindingContext if a ViewModel is passed.
        /// </summary>
        /// <returns>Task representing the navigation</returns>
        /// <param name="pageKey">Page key.</param>
        /// <param name="viewModel">View model.</param>
        public Task NavigateAsync(object pageKey, object viewModel = null)
        {
            if (pageKey == null)
                throw new ArgumentNullException (nameof (pageKey));

            // On a phone, always hide master page when we navigate since we
            // will be using the Detail page.
            if (HideMasterPageOnNavigation)
            {
                var mdPage = Application.Current.MainPage as MasterDetailPage;
                if (mdPage != null)
                {
                    mdPage.IsPresented = false;
                }
            }

            // Look for a registered page first. If that's not available, look for an action.
            var page = GetPageByKey(pageKey);
            if (page == null) {
                if (_registeredActions != null)
                {
                    Action<object> work;
                    if (_registeredActions.TryGetValue(pageKey, out work))
                    {
                        work.Invoke(viewModel);
                    }
                }
                return TaskCompleted;
            }

            if (viewModel != null)
                page.BindingContext = viewModel;

            return Navigation.PushAsync(page);
        }

        /// <summary>
        /// True if we can go backwards on the navigation stack.
        /// </summary>
        /// <value><c>true</c> if can go back; otherwise, <c>false</c>.</value>
        public bool CanGoBack => Navigation.NavigationStack.Count > 1;

        /// <summary>
        /// Pops the last page off the stack.
        /// </summary>
        /// <returns>Task representing the navigation event.</returns>
        public Task GoBackAsync() => !CanGoBack ? TaskCompleted : Navigation.PopAsync();

        /// <summary>
        /// Pushes a new page modally onto the navigation stack.
        /// </summary>
        /// <returns>Task representing the modal navigation.</returns>
        /// <param name="pageKey">Page key.</param>
        /// <param name="viewModel">View model.</param>
        public Task PushModalAsync(object pageKey, object viewModel = null)
        {
            if (pageKey == null)
                throw new ArgumentNullException (nameof (pageKey));

            var page = GetPageByKey(pageKey);
            if (page == null)
                throw new ArgumentException("Cannot navigate to unregistered page", nameof(pageKey));

            if (viewModel != null)
                page.BindingContext = viewModel;

            return Navigation.PushModalAsync(page);
        }

        /// <summary>
        /// Pops a page off the modal stack.
        /// </summary>
        /// <returns>Task representing the navigation.</returns>
        public Task PopModalAsync() => Navigation.PopModalAsync();
    }
}

