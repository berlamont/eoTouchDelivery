﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eoTouchDelivery.Core.Effects;
using eoTouchDelivery.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(MaxLinesEffect), "MaxLinesEffect")]
namespace eoTouchDelivery.iOS.Effects
{
    public class MaxLinesEffect : PlatformEffect
    {
        UILabel _control;

        protected override void OnAttached()
        {
            _control = Control as UILabel;
            SetMaxLines();
        }

        protected override void OnDetached()
        {
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            if (args.PropertyName == NumberOfLinesEffect.NumberOfLinesProperty.PropertyName)
            {
                SetMaxLines();
            }
        }

        private void SetMaxLines()
        {
            var maxLines = NumberOfLinesEffect.GetNumberOfLines(Element);

            if (_control != null)
            {
                _control.Lines = maxLines;
            }
        }
    }
}
