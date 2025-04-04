using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eoTouchDelivery.Core.Models
{
    class TruckInspectionModel
    {
        public string InspectinType { get; set; }
        public string DateTime { get; set; }
        public string InspectionStatus { get; set; }
        public string InspectionScore { get; set; }
    }   
}
public static class WorkStatus
{
    public const string Required = "Failed Maintenance Required";
    public const string Corrected = "Vehicale Defects Corrected";
    public const string FixNeeded = "Defects Found No Fixes Needed";

}

