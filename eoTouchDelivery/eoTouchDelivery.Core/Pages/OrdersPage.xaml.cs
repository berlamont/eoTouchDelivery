using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace eoTouchDelivery.Core.Pages
{
    public partial class OrdersPage : ContentPage
    {
        public OrdersPage()
        {
            InitializeComponent();
            var tapGestureRecognizer = new TapGestureRecognizer();
            List<string> list = new List<string>(new string[] { "WH MGR 123", "WH MGR 12", "WH MGR 33", "WH MGR WD23", "WILLIAM OD", "MIKE OP", "JEFF SK", "ROUTE 435", "ROUTE 815", "ROUTE 4", "ROUTE 8",  });
            tapGestureRecognizer.Tapped += async (s, e) =>
            {
                var action = await DisplayActionSheet(" Check In as :", "Cancel", null, list.ToArray());
            };
            _Framelayout.GestureRecognizers.Add(tapGestureRecognizer);
        }
    }
}
