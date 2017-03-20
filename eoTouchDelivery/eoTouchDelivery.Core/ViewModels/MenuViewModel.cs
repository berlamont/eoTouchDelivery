using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using eoTouchDelivery.Core.Helpers;
using eoTouchDelivery.Core.Interfaces;
using eoTouchDelivery.Core.Services;
using eoTouchDelivery.Models.Enums;
using eoTouchDelivery.Models.Users;
using Xamarin.Forms;
using MenuItem = eoTouchDelivery.Models.MenuItem;

namespace eoTouchDelivery.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public ICommand ItemSelectedCommand => new Command<MenuItem>(OnSelectItem);

        public ICommand LogoutCommand => new Command(OnLogout);

        ObservableCollection<MenuItem> menuItems = new ObservableCollection<MenuItem>();

        public ObservableCollection<MenuItem> MenuItems
        {
            get
            {
                return menuItems;
            }
            set
            {
                menuItems = value;
                RaisePropertyChanged(() => MenuItems);
            }
        }
        
        public MenuViewModel()
        {
          
            InitMenuItems();

        }

        void InitMenuItems()
        {
            if (Device.OS == TargetPlatform.Windows)
            {
                MenuItems.Add(new MenuItem
                {
                    Title = "Home",
                    MenuItemType = MenuItemType.Home,
                    ViewModelType = typeof(MainViewModel),
                    IsEnabled = true
                });

                MenuItems.Add(new MenuItem
                {
                    Title = "New Ride",
                    MenuItemType = MenuItemType.NewRide,
                    ViewModelType = typeof(CustomRideViewModel),
                    IsEnabled = true
                });
            }

            if (Device.OS == TargetPlatform.Android)
            {
                MenuItems.Add(new MenuItem
                {
                    Title = "New Ride",
                    MenuItemType = MenuItemType.NewRide,
                    ViewModelType = typeof(CustomRideViewModel),
                    IsEnabled = true
                });
            }

            MenuItems.Add(new MenuItem
            {
                Title = "Report",
                MenuItemType = MenuItemType.Reports,
                ViewModelType = typeof(ReportsViewModel),
                IsEnabled = Settings.CurrentBookingId != 0
            });

            MenuItems.Add(new MenuItem
            {
                Title = "Profile",
                MenuItemType = MenuItemType.Profile,
                ViewModelType = typeof(ProfileViewModel),
                IsEnabled = true
            });
        }

        private async void OnSelectItem(MenuItem item)
        {
            if (item.IsEnabled)
            {
                object parameter = null;

                await NavigationService.NavigateToAsync(item.ViewModelType, parameter);
            }
        }

        private async void OnLogout()
        {
            await _authenticationService.LogoutAsync();
            await NavigationService.NavigateToAsync<LoginViewModel>();
        }

        private void OnBookingRequested(Booking booking)
        {
            SetMenuItemStatus(MenuItemType.UpcomingRide, true);
            SetMenuItemStatus(MenuItemType.ReportIncident, true);
        }

        private void OnBookingFinished(Booking booking)
        {
            SetMenuItemStatus(MenuItemType.UpcomingRide, false);
        }

        private void SetMenuItemStatus(MenuItemType type, bool enabled)
        {
            MenuItem rideItem = MenuItems.FirstOrDefault(m => m.MenuItemType == type);

            if (rideItem != null)
            {
                rideItem.IsEnabled = enabled;
            }
        }

        private async void OnProfileUpdated(UserProfile profile)
        {
            Profile = null;

            if (Device.OS == TargetPlatform.Windows)
            {
                await Task.Delay(2000); // Give UWP enough time (for Photo reload)
            }

            Profile = profile;
        }
    }
}