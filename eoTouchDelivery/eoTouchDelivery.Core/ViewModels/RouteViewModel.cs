using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace eoTouchDelivery.Core.ViewModels
{
    public class RouteViewModel : ContentPage
	{
		public Command test
		{
			get
			{
				return new Command(async () =>
				                   {
					                   List<string> popuplist = new List<string>(new string[] { "Orders Recap", "Pallet Breakdown", "Review Invoices" });
					                   var action = await DisplayActionSheet(" Select Delivery Report", "Cancel", null, popuplist.ToArray());
				                   });
			}

		}
		private string _dateTime;
		public string Datetime
		{
			get { return _dateTime; }
			set { _dateTime = value; }
		}

	}
}
