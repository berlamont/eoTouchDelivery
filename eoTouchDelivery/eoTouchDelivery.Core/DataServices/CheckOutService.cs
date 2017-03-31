using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eoTouchDelivery.Core.Models;

namespace eoTouchDelivery.Core.DataServices
{
    public class CheckOutService
    {
		public static List<CheckOutModel> GetCheckOutModel()
	    {
		    List<CheckOutModel> listValues = new List<CheckOutModel>();
		    listValues.Add(new CheckOutModel() { productName = "OLD MIL LIGHT2/12PK", unit = "CASE", number = "123454545", qty = "5" });
		    listValues.Add(new CheckOutModel() { productName = "OLD MIL LIGHT2/12PK", unit = "CASE", number = "123454545", qty = "" });
		    listValues.Add(new CheckOutModel() { productName = "OLD MIL LIGHT2/12PK", unit = "CASE", number = "123454545", qty = "4" });
		    listValues.Add(new CheckOutModel() { productName = "OLD MIL LIGHT2/12PK", unit = "CASE", number = "123454545", qty = "" });
		    listValues.Add(new CheckOutModel() { productName = "OLD MIL LIGHT2/12PK", unit = "CASE", number = "123454545", qty = "" });
		    listValues.Add(new CheckOutModel() { productName = "OLD MIL LIGHT2/12PK", unit = "CASE", number = "123454545", qty = "8" });
		    listValues.Add(new CheckOutModel() { productName = "OLD MIL LIGHT2/12PK", unit = "CASE", number = "123454545", qty = "" });
		    listValues.Add(new CheckOutModel() { productName = "OLD MIL LIGHT2/12PK", unit = "CASE", number = "123454545", qty = "3" });
		    listValues.Add(new CheckOutModel() { productName = "OLD MIL LIGHT2/12PK", unit = "CASE", number = "123454545", qty = "" });
		    listValues.Add(new CheckOutModel() { productName = "OLD MIL LIGHT2/12PK", unit = "CASE", number = "123454545", qty = "8" });
		    listValues.Add(new CheckOutModel() { productName = "OLD MIL LIGHT2/12PK", unit = "CASE", number = "123454545", qty = "" });
		    listValues.Add(new CheckOutModel() { productName = "OLD MIL LIGHT2/12PK", unit = "CASE", number = "123454545", qty = "" });

		    return listValues;
	    }
    }
}