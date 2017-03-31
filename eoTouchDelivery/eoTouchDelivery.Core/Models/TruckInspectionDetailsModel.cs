using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eoTouchDelivery.Core.Models
{
   public  class TruckInspectionDetailsModel
    {
        public string TruckNumber { get; set; }
        public string TrailerNumber { get; set; }
        public InspectionResult TruckCondition { get; set; }
        public InspectionResult InspectionItems { get; set; }
        public string OdometerReading { get; set; }
        public string Temperature { get; set; }
        public string Notes { get; set; }
        public string Signature { get; set; }
       
    }
    public class InspectionResult
    {
        public string Message { get; set; }
        public bool Status { get; set; }
    }
    
}
