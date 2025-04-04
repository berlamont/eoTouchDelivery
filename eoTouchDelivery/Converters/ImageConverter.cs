using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace eoTouchDelivery.Core.Converters
{
    public class ImageConverter : IValueConverter
	{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value == null)
			return null;
		switch (value.ToString())
		{
			case WorkStatus.Required:
				return "DangerIcon.png";

			case WorkStatus.Corrected:
				return null;

			case WorkStatus.FixNeeded:
				return null;
			default:
				return null;
		}
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return null;
	}
	}
}
