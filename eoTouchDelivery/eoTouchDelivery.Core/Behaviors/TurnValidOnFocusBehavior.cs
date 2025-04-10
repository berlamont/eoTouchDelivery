﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eoTouchDelivery.Core.Validations;
using Xamarin.Forms;

namespace eoTouchDelivery.Core.Behaviors
{
	public class TurnValidOnFocusBehavior : Behavior<Entry>
	{
		public static readonly BindableProperty ValidityObjectProperty =
			BindableProperty.Create("ValidityObject", typeof(IValidity), typeof(TurnValidOnFocusBehavior), null);

		public IValidity ValidityObject
		{
			get { return (IValidity)GetValue(ValidityObjectProperty); }
			set { SetValue(ValidityObjectProperty, value); }
		}

		private Entry _entry;

		protected override void OnAttachedTo(Entry bindable)
		{
			base.OnAttachedTo(bindable);
			_entry = bindable;
			bindable.BindingContextChanged += OnBindingContextChanged;

			bindable.Focused += OnEntryFocused;
		}

		private void OnBindingContextChanged(object sender, System.EventArgs e)
		{
			BindingContext = _entry?.BindingContext;
		}

		protected override void OnDetachingFrom(Entry bindable)
		{
			_entry = null;
			BindingContext = null;
			bindable.BindingContextChanged -= OnBindingContextChanged;
			bindable.Focused -= OnEntryFocused;
			base.OnDetachingFrom(bindable);
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
		}

		private void OnEntryFocused(object sender, FocusEventArgs e)
		{
			if (e.IsFocused && ValidityObject != null)
			{
				ValidityObject.IsValid = true;
			}
		}
	}
}
