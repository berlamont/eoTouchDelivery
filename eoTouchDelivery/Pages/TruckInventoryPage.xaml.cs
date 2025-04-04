using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace eoTouchDelivery.Core.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TruckInventoryPage : TabbedPage
	{
		public TruckInventoryPage()
		{
			InitializeComponent();
		}

		void Toolbar_OnClicked(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}
