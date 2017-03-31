namespace eoTouchDelivery.Core.Models
{
  public class DeliveryModel
    {
        public string AuthorizedPersonName { get; set; }
        public string Date { get; set; }
        public string ItemName { get; set; }
        public string  Location { get; set; }
        public string  Name { get; set; }
        public string Pin { get; set; }
        public onTruckItems TruckItems { get; set; }
        public DeliveredItems deliveredItem { get; set; } 
    }
    public class onTruckItems
    {
        public string  Cases { get; set; }
        public string Kegs { get; set; }
        public string Others { get; set; }
    }
    public class DeliveredItems
    {
        public string Cases { get; set; }
        public string Kegs { get; set; }
        public string Others { get; set; }
    }
}
