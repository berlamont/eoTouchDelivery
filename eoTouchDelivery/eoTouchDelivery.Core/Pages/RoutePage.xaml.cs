using eoTouchDelivery.Core.Models;
using eoTouchDelivery.Core.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eoTouchDelivery.Core.ViewModels;
using Microsoft.VisualBasic;
using Xamarin.Forms;

namespace eoTouchDelivery.Core.Pages
{
    
    public partial class RoutePage : ContentPage
    {
        private RouteViewModel _vm;
        private Command cmd;
		
        protected override void OnAppearing()
        {
            _datepic.IsVisible = false;

            List<RouteDate> routData = new List<RouteDate>();
            routData.Add(new RouteDate() { Day = "Yesterday", Date = DateTime.Today.AddDays(-1), FormattedDate = DateTime.Today.AddDays(-1).ToString("dddd MMM d, yyy") });
            routData.Add(new RouteDate() { Day = "Today", Date = DateTime.Today, FormattedDate = DateTime.Today.ToString("dddd MMM d, yyy") });
            routData.Add(new RouteDate(){Day = "Tomorrow",Date = DateTime.Today.AddDays(1),FormattedDate = DateTime.Today.AddDays(1).ToString("dddd MMM d, yyy")});

            List<RouteCustomer> routeCustomer = new List<RouteCustomer>();
            routeCustomer.Add(new RouteCustomer()
            {
                CustomerName = "Lunds & Byerlys",
                StopStatusDescription = "Delivered",
                StopStatusIcon = "Delivered",
                StopCaseCount = 28,
                StopKegCount = 24,
                StopOtherCount = 12
            });
            routeCustomer.Add(new RouteCustomer()
            {
                CustomerName = "Total Wine & More",
                StopStatusDescription = "Delivered",
                StopStatusIcon = "Delivered",
                StopCaseCount = 389,
                StopKegCount = 1,
                StopOtherCount = 7
            });
            routeCustomer.Add(new RouteCustomer()
            {
                CustomerName = "MGM Liquor Warehouse",
                StopStatusDescription = "In Progress",
                StopStatusIcon = "InProgress",
                StopCaseCount = 82,
                StopKegCount = 51,
                StopOtherCount = 145
            });
            routeCustomer.Add(new RouteCustomer()
            {
                CustomerName = "Surdyk's Liquor & Cheese Shop",
                StopStatusDescription = "To Be Delivered",
                StopStatusIcon =  "To Be Delivered",
                StopCaseCount = 261,
                StopKegCount = 621,
                StopOtherCount = 32
            });
            routeCustomer.Add(new RouteCustomer()
            {
                CustomerName = "Kowalski's Market & Wine Shop",
                StopStatusDescription = "Attention",
                StopStatusIcon ="Attention",
                StopCaseCount = 42,
                StopKegCount = 23,
                StopOtherCount = 522
            });
            routeCustomer.Add(new RouteCustomer()
            {
                CustomerName = "Happy Harry’s Bottle Shop",
                StopStatusDescription = "Off Truck",
                StopStatusIcon = "Off Truck",
                StopCaseCount = 261,
                StopKegCount = 621,
                StopOtherCount = 32
            });
            routeCustomer.Add(new RouteCustomer()
            {
                CustomerName = "Ramirez Liquor & Kegs",
                StopStatusDescription = "No Deliveries",
                StopStatusIcon =  "No Deliveries",
                StopCaseCount = 0,
                StopKegCount = 0,
                StopOtherCount = 0
            });
            _routeDataList.ItemsSource = routeCustomer;
            _vm.Datetime = DateTime.Now.ToString(Constants.DATE_TIME_PICKER_DATE_FORMAT);         
        }      
        
        public RoutePage()
        {
            InitializeComponent();
            this.BindingContext = new RouteViewModel();
            _vm = new RouteViewModel();
            
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                _datepic.Focus();
            };
            _dateStack.GestureRecognizers.Add(tapGestureRecognizer);
            _datepic.DateSelected += _datepic_DateSelected;            
        }        

        private void _datepic_DateSelected(object sender, DateChangedEventArgs e)
        {
            _vm.Datetime = e.NewDate.ToString(Constants.DATE_TIME_PICKER_DATE_FORMAT);
            _dateLabel.Text = _vm.Datetime;
        }
    }
}
