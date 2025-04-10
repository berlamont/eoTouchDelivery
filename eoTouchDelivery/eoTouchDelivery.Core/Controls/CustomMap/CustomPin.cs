﻿using Xamarin.Forms.Maps;

namespace eoTouchDelivery.Core.Controls
{
	public class CustomPin
	{
		public enum AnnotationType
		{
			Normal,
			From,
			To
		}

		public int Id { get; set; }

		public string Label { get; set; }

		public string Address { get; set; }

		public string PinIcon { get; set; }

		public Position Position { get; set; }

		public AnnotationType Type { get; set; }

		public override string ToString() => Label;
	}
}