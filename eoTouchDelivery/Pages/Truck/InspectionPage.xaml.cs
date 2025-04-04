using eoTouchDelivery.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace eoTouchDelivery.Core.Pages.Truck
{
    public partial class InspectionPage : ContentPage
    {
        public InspectionPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            List<TruckInspectionModel> InspectionList = new List<TruckInspectionModel>();
            InspectionList.Add(new TruckInspectionModel() { InspectinType = "Beginning of Day Inspection", DateTime = "10/05/17 0830", InspectionStatus = "Condition Satisfactory", InspectionScore = "#2453" });
            InspectionList.Add(new TruckInspectionModel() { InspectinType = "End of Day Inspection ", DateTime = "10/05/17 1930", InspectionStatus = "Vehicle Defects Corrected", InspectionScore = "#2455" });
            InspectionList.Add(new TruckInspectionModel() { InspectinType = "Driver Inspection", DateTime = "09/20/17 1930", InspectionStatus = "Defects Found No Fixes Needed", InspectionScore = "#2457" });
            _driverInspectionList.ItemsSource = InspectionList;

            List<TruckInspectionModel> MechanicInspectionList = new List<TruckInspectionModel>();
            MechanicInspectionList.Add(new TruckInspectionModel() { InspectinType = "Beginning of Day Inspection", DateTime = "10/05/17 0830", InspectionStatus = "Failed Maintenance Required", InspectionScore = "#2453" });
            MechanicInspectionList.Add(new TruckInspectionModel() { InspectinType = "End of Day Inspection", DateTime = "10/05/17 1930", InspectionStatus = "Vehicle Defects Corrected", InspectionScore = "#2455" });
            MechanicInspectionList.Add(new TruckInspectionModel() { InspectinType = "Failed Maintenance Required", DateTime = "09/20/17 1930", InspectionStatus = "Defects Found No Fixes Needed", InspectionScore = "#2457" });
            _mechanicInspectionList.ItemsSource = MechanicInspectionList;
        }
    }
}
