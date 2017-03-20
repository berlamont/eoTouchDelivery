using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace eoTouchDelivery.Core.Controls
{
	public class CustomMap : Map
	{
		public static readonly BindableProperty SelectedPinProperty =
			BindableProperty.Create("SelectedPin",
				typeof(CustomPin), typeof(CustomMap), null);

		public static readonly BindableProperty FromProperty =
			BindableProperty.Create("From",
				typeof(CustomPin), typeof(CustomMap), null);

		public static readonly BindableProperty ToProperty =
			BindableProperty.Create("To",
				typeof(CustomPin), typeof(CustomMap), null);

		public static readonly BindableProperty CustomPinsProperty =
			BindableProperty.Create("CustomPins",
				typeof(IEnumerable<CustomPin>), typeof(CustomMap), default(IEnumerable<CustomPin>),
				BindingMode.TwoWay);

		public CustomPin SelectedPin
		{
			get => (CustomPin) GetValue(SelectedPinProperty);
			set => SetValue(SelectedPinProperty, value);
		}

		public CustomPin From
		{
			get => (CustomPin) GetValue(FromProperty);
			set => SetValue(FromProperty, value);
		}

		public CustomPin To
		{
			get => (CustomPin) GetValue(ToProperty);
			set => SetValue(ToProperty, value);
		}

		public IEnumerable<CustomPin> CustomPins
		{
			get => (IEnumerable<CustomPin>) GetValue(CustomPinsProperty);
			set => SetValue(CustomPinsProperty, value);
		}
	}
}