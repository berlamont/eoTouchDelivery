using System;
using System.Collections;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace eoTouchDelivery.Core.Controls
{
	public class BindablePicker : Picker
	{
		public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource",
			typeof(IEnumerable), typeof(BindablePicker), null, propertyChanged: OnItemsSourceChanged);

		public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create("SelectedItem",
			typeof(object), typeof(BindablePicker), null, BindingMode.TwoWay, propertyChanged: OnSelectedItemChanged);

		public BindablePicker()
		{
			SelectedIndexChanged += (o, e) =>
			{
				if (SelectedIndex < 0 || ItemsSource == null || !ItemsSource.GetEnumerator().MoveNext())
				{
					SelectedItem = null;
					return;
				}

				var index = 0;
				foreach (var item in ItemsSource)
				{
					if (index == SelectedIndex)
					{
						SelectedItem = item;
						break;
					}
					index++;
				}
			};
		}

		public IEnumerable ItemsSource
		{
			get => (IEnumerable) GetValue(ItemsSourceProperty);
			set => SetValue(ItemsSourceProperty, value);
		}

		public object SelectedItem
		{
			get => GetValue(SelectedItemProperty);
			set
			{
				if (SelectedItem == value)
					return;
				SetValue(SelectedItemProperty, value);
				InternalUpdateSelectedIndex();
			}
		}

		public event EventHandler<SelectedItemChangedEventArgs> ItemSelected;

		void InternalUpdateSelectedIndex()
		{
			var selectedIndex = -1;
			if (ItemsSource != null)
			{
				var index = 0;
				foreach (var item in ItemsSource)
				{
					var strItem = item?.ToString();

					if (item != null && SelectedItem != null
					    && !string.IsNullOrEmpty(strItem)
					    && item.ToString().Equals(SelectedItem.ToString()))
					{
						selectedIndex = index;
						break;
					}
					index++;
				}
			}

			SelectedIndex = selectedIndex;
		}

		static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var boundPicker = (BindablePicker) bindable;

			if (Equals(newValue, null) && !Equals(oldValue, null))
				return;

			boundPicker.Items.Clear();

			if (!Equals(newValue, null))
				foreach (var item in (IEnumerable) newValue)
					boundPicker.Items.Add(item.ToString());

			boundPicker.InternalUpdateSelectedIndex();
		}

		static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var boundPicker = (BindablePicker) bindable;
			boundPicker.ItemSelected?.Invoke(boundPicker, new SelectedItemChangedEventArgs(newValue));
			boundPicker.InternalUpdateSelectedIndex();
		}
	}
}