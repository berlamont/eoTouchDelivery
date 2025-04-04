using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapKit;

namespace eoTouchDelivery.iOS.Renderers
{
    public class CustomMKAnnotationView : MKAnnotationView
    {
        public int Id { get; set; }

        public CustomMKAnnotationView(IMKAnnotation annotation, int id)
            : base(annotation, id.ToString())
        {
        }
    }
}