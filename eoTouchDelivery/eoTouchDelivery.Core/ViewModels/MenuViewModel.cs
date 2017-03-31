using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using eoTouchDelivery.Core.Models.Users;
using eoTouchDelivery.Core.Services;
using eoTouchDelivery.Core.Utils;
using eoTouchDelivery.Core.Models.Enums;
using Xamarin.Forms;
using MenuItem = eoTouchDelivery.Core.Models.MenuItem;

namespace eoTouchDelivery.Core.ViewModels
{
    public class MenuViewModel : ViewModelBase
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

            }

            if (Device.OS == TargetPlatform.Android)
            {
                MenuItems.Add(new MenuItem
                {
                    Title = "New Ride",
                    MenuItemType = MenuItemType.Home,
                    ViewModelType = typeof(MainViewModel),
                    IsEnabled = true
                });
            }

            MenuItems.Add(new MenuItem
            {
                Title = "Report",
                MenuItemType = MenuItemType.Reports,
                ViewModelType = typeof(ReportsViewModel),
                IsEnabled = Settings.SupportBit != 0
            });

        }

	    async void OnSelectItem(MenuItem item)
        {
            if (item.IsEnabled)
            {
                object parameter = null;

                await NavigationService.NavigateToAsync(item.ViewModelType, parameter);
            }
        }

	    async void OnLogout()
        {
            await NavigationService.NavigateToAsync<LoginViewModel>();
        }

	    void SetMenuItemStatus(MenuItemType type, bool enabled)
        {
            MenuItem rideItem = MenuItems.FirstOrDefault(m => m.MenuItemType == type);

            if (rideItem != null)
            {
                rideItem.IsEnabled = enabled;
            }
        }
    }
}