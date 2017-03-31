using eoTouchDelivery.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace eoTouchDelivery.Core.Pages
{
    public partial class SettingsPage : ContentPage
    {
        protected override void OnAppearing()
        {
            List<SettingsModel> settingslistValues = new List<SettingsModel>();
            settingslistValues.Add(new SettingsModel() { Activity = "Upload from scratch-pad", ActivityValue = "1 order and one more..." });
            settingslistValues.Add(new SettingsModel() { Activity = "Work in progress", ActivityValue = "4 deliveries" });
            settingslistValues.Add(new SettingsModel() { Activity = "Logged in as", ActivityValue = "Route 435" });
            settingslistValues.Add(new SettingsModel() { Activity = "Archives", ActivityValue = "HH 138: Ruherdford Associates" });
            settingslistValues.Add(new SettingsModel() { Activity = "Other Settings ", ActivityValue = "Navigate to Android Settings" });
            settingslistValues.Add(new SettingsModel() { Activity = "Help & feedback", ActivityValue = "Contact Support" });
            _settingsdataList.ItemsSource = settingslistValues;
            
        }
        public SettingsPage()
        {
            
            InitializeComponent();

            _settingsdataList.ItemSelected += _settingsdataList_ItemSelected;           


        }

        private void _settingsdataList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            ((ListView)sender).SelectedItem = null;
        }
    }
}
