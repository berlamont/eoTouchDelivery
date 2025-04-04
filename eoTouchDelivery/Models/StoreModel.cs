using System;
namespace eoTouchDelivery.Core.Models
{
	public class StoreModel
	{
		public int Id { get; set; }
		public double sourceLat { get; set; }
		public double sourceLong { get; set; }
		public double DestinationLat { get; set; }
		public double DestinationLong { get; set; }
		public string MarkerImage { get; set; }

	}
}
