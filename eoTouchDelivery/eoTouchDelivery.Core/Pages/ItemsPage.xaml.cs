using System;
using System.Collections.Generic;
using eoTouchDelivery.Core.Models;
using Xamarin.Forms;

namespace eoTouchDelivery.Core.Pages
{
    public partial class ItemsPage : ContentPage
    {
		public ItemsPage()
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
		protected override void OnAppearing()
		{
			base.OnAppearing();
			List<CheckOutModel> listValues = new List<CheckOutModel>();
			listValues.Add(new CheckOutModel() { productName = "OLD MIL LIGHT2/12PK", unit = "CASE", number = "123454545", qty = "5" });
			listValues.Add(new CheckOutModel() { productName = "OLD MIL LIGHT2/12PK", unit = "CASE", number = "123454545", qty = "" });
			listValues.Add(new CheckOutModel() { productName = "OLD MIL LIGHT2/12PK", unit = "CASE", number = "123454545", qty = "4" });
			listValues.Add(new CheckOutModel() { productName = "OLD MIL LIGHT2/12PK", unit = "CASE", number = "123454545", qty = "" });
			listValues.Add(new CheckOutModel() { productName = "OLD MIL LIGHT2/12PK", unit = "CASE", number = "123454545", qty = "" });
			listValues.Add(new CheckOutModel() { productName = "OLD MIL LIGHT2/12PK", unit = "CASE", number = "123454545", qty = "8" });
			listValues.Add(new CheckOutModel() { productName = "OLD MIL LIGHT2/12PK", unit = "CASE", number = "123454545", qty = "" });
			listValues.Add(new CheckOutModel() { productName = "OLD MIL LIGHT2/12PK", unit = "CASE", number = "123454545", qty = "3" });
			listValues.Add(new CheckOutModel() { productName = "OLD MIL LIGHT2/12PK", unit = "CASE", number = "123454545", qty = "" });
			listValues.Add(new CheckOutModel() { productName = "OLD MIL LIGHT2/12PK", unit = "CASE", number = "123454545", qty = "8" });
			listValues.Add(new CheckOutModel() { productName = "OLD MIL LIGHT2/12PK", unit = "CASE", number = "123454545", qty = "" });
			listValues.Add(new CheckOutModel() { productName = "OLD MIL LIGHT2/12PK", unit = "CASE", number = "123454545", qty = "" });
			_dataList.ItemsSource = listValues;
		}
	}
}
