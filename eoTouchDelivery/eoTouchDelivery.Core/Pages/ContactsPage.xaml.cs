using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace eoTouchDelivery.Core.Pages
{
    public partial class ContactsPage : ContentPage
    {
        public ContactsPage()
        {
            InitializeComponent();
            var tapGestureRecognizer = new TapGestureRecognizer();
            List<string> list = new List<string>(new string[] { "String1", "String2", "String1", "String2", "String1", "String2", "String1", "String2", "String1", "String2", "String1", "String2", });
            tapGestureRecognizer.Tapped += async (s, e) =>
            {
                await Navigation.PushAsync(new RoutePage());
                // 
                //var action = await DisplayActionSheet(" Choose a Truck Strip/Count Startegy:", "Cancel", null, list.ToArray());
                 
            };
            _jackShephardlayout.GestureRecognizers.Add(tapGestureRecognizer);

            _goButton.Clicked += _goButton_Clicked;

            var GridRecognizer = new TapGestureRecognizer();
            GridRecognizer.Tapped += async (s, e) =>
            {
                var action = await DisplayActionSheet(" Choose a Truck Strip/Count Startegy:", "Cancel", null, "BLIND", "NON-BLIND");
            };
            OnTruckGrid.GestureRecognizers.Add(GridRecognizer);
        }

        private async void _goButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MapPage());
        }
    }
}
