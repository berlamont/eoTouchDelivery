
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eoTouchDelivery.Core.DataServices;
using eoTouchDelivery.Core.Models;
using Xamarin.Forms;

namespace eoTouchDelivery.Core.Pages
{
    public partial class CheckOutPage : ContentPage
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();
            List<CheckOutModel> listValues = new List<CheckOutModel>();
            listValues = CheckOutService.GetCheckOutModel();
            _dataList.ItemsSource = listValues;
        }
        public CheckOutPage()
        {
            InitializeComponent();
            Toolbar.Icon = "plus.png";
            _dataList.ItemSelected += _dataList_ItemSelected;
        }

        private void _dataList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            ((ListView)sender).SelectedItem = null;
        }
    }
}
