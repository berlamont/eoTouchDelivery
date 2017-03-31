using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eoTouchDelivery.Core;
using eoTouchDelivery.Core.Models;
using eoTouchDelivery.Core.Pages;
using Xamarin.Forms;

namespace eoTouchDelivery.Core.Pages
{
    
    public partial class MasterPage : ContentPage
    {
       // eoTouchDelivery.Core.MainPage mainPage;
        public ListView StartOfDayList { get { return _StartOfDayList; } }
        public ListView MyDaytList { get { return _myDayList; } }
        public ListView EndOfDayList { get { return _EndDayList; } }

        public ListView HomeList { get { return _homeList; } }
        public MasterPage()
        {
            InitializeComponent();
            
            HeaderStack.HeightRequest = 100;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            List<MenuItemMode> HomeList = new List<MenuItemMode>();
            HomeList.Add(new MenuItemMode() { IconSource = "home.png", Title = "Home", TargetPage = typeof(ContactsPage) });
            _homeList.ItemsSource = HomeList;

            //Start of day items
            List<MenuItemMode> StartOfDayList = new List<MenuItemMode>();
            StartOfDayList.Add(new MenuItemMode() { IconSource = "spanner.png", Title = "Truck Inspection" , TargetPage = typeof(TruckInspectionPage) });
            StartOfDayList.Add(new MenuItemMode() { IconSource = "checkout.png", Title = "Check Out" , TargetPage = typeof(CheckOutPage)});
            _StartOfDayList.ItemsSource = StartOfDayList;


            //My day items
            List<MenuItemMode> MyDayList = new List<MenuItemMode>();
            MyDayList.Add(new MenuItemMode() { IconSource = "route.png", Title = "Route", TargetPage = typeof(RoutePage) });
            MyDayList.Add(new MenuItemMode() { IconSource = "truck.png", Title = "Truck Inventory", TargetPage = typeof(TruckInventoryPage) });
            MyDayList.Add(new MenuItemMode() { IconSource = "graph.png", Title = "Reports", TargetPage = typeof(ReportsPage) });
            _myDayList.ItemsSource = MyDayList;


            //End of day items
            List<MenuItemMode> EndOfDayList = new List<MenuItemMode>();
            EndOfDayList.Add(new MenuItemMode() { IconSource = "spanner.png", Title = "Truck Inspection", TargetPage = typeof(TruckInspectionPage) });
            EndOfDayList.Add(new MenuItemMode() { IconSource = "checkin.png", Title = "Check In", TargetPage = typeof(CheckInTabbedPage) });
            EndOfDayList.Add(new MenuItemMode() { IconSource = "gear.png", Title = "Settings", TargetPage = typeof(SettingsPage) });
            _EndDayList.ItemsSource = EndOfDayList;
        }
    }
}
