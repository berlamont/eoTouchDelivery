using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eoTouchDelivery.Core.Models;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace eoTouchDelivery.Core.Converters
{
    public class StatusImageConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
				return null;
			switch (value.ToString())
			{
				case StopStatus.Delivered:
					return "delivered.png";

				case StopStatus.InProgress:
					return "inprogress.png";

				case StopStatus.Attention:
					return "attention.png";
				default:
					return "default.png";
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}