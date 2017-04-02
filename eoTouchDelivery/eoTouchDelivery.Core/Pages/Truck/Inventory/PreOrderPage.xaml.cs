using eoTouchDelivery.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace eoTouchDelivery.Core.Pages.Truck.Inventory
{ 
	public partial class PreOrderPage : ContentPage
	{
		public PreOrderPage()
		{
			InitializeComponent();
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();

			List<TruckInventoryModel> listValues = new List<TruckInventoryModel>();
			listValues.Add(new TruckInventoryModel() { ProductName = "OLD MIL LIGHT2/12PK", ProductType = "CASE", ProductNumber = "98783358", Quantity = "1" });
			listValues.Add(new TruckInventoryModel() { ProductName = "Corona 18pk BTL 12 OZ", ProductType = "KEG", ProductNumber = "09504465", Quantity = "1" });
			listValues.Add(new TruckInventoryModel() { ProductName = "KEYSTONE ICE 30 PK CAN 12OZ", ProductType = "PACK", ProductNumber = "03509218", Quantity = "1" });
			listValues.Add(new TruckInventoryModel() { ProductName = "Blue MN BELG 2/12PK", ProductType = "PACK", ProductNumber = "34393502", Quantity = "1" });
			listValues.Add(new TruckInventoryModel() { ProductName = "HEINEKEN 24 PK BTL 11.5 OZ", ProductType = "CASE", ProductNumber = "83489219", Quantity = "1" });
			listValues.Add(new TruckInventoryModel() { ProductName = "LABBAT BLUE 28 PK BTL 11.5 OZ", ProductType = "CASE", ProductNumber = "90988235", Quantity = "1" });
			listValues.Add(new TruckInventoryModel() { ProductName = "Corona 18pk BTL 12 OZ", ProductType = "KEG", ProductNumber = "47917109", Quantity = "1" });
			listValues.Add(new TruckInventoryModel() { ProductName = "Blue MN BELG 2/12PK", ProductType = "PACK", ProductNumber = "94566847", Quantity = "1" });
			listValues.Add(new TruckInventoryModel() { ProductName = "KEYSTONE ICE 30 PK CAN 12OZ", ProductType = "CASE", ProductNumber = "83465371", Quantity = "1" });
			listValues.Add(new TruckInventoryModel() { ProductName = "Corona 18pk BTL 12 OZ", ProductType = "PACK", ProductNumber = "25460837", Quantity = "1" });
			_dataList.ItemsSource = listValues;
		}
	}
}
