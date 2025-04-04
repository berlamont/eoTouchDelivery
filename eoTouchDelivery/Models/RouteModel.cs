using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

	namespace eoTouchDelivery.Core.Models
{
    public class Route
    {
        //Yesterday, Today or Tomorrow only
        public string RouteDayContext { get; set; }
        public DateTime RouteDateContext { get; set; }
        //DateTime to be displayed in the format "Thursday February 9, 2017"
        public string RouteFormattedDateContext { get; set; }
        public int TotalCustomerCount { get; set; }
        public int TotalStopCount { get; set; }
        public int TotalCaseCount { get; set; }
        public int TotalKegCount { get; set; }
        public int TotalOtherCount { get; set; }
    }

    public class RouteCustomer
    {
        public string CustomerName { get; set; }
        public string StopStatusIcon { get; set; }
        public string StopStatusDescription { get; set; }
        public int StopCaseCount { get; set; }
        public int StopKegCount { get; set; }
        public int StopOtherCount { get; set; }
    }
    public class RouteDate
    {
        public string Day { get; set; }
        public DateTime Date { get; set; }
        public string FormattedDate { get; set; }
    }
    public static class StopStatus
    {
        public const string Delivered = "Delivered";
        public const string InProgress = "InProgress";
        public const string Attention = "Attention";

    }

}
