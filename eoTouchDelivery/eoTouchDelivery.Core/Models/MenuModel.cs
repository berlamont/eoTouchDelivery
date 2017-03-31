using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

	namespace eoTouchDelivery.Core.Models
{
   public class MenuModel
    {
        public string Heading { get; set; }
        public List<MasterPageItem> Items { get; set; }
    }
}
