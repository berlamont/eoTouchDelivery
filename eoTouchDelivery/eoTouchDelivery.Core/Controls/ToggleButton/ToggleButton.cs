﻿using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace eoTouchDelivery.Core.Controls
{
    public class ToggleButton : ContentView
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(ToggleButton), null);

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create("CommandParameter", typeof(object), typeof(ToggleButton), null);

        public static readonly BindableProperty CheckedProperty =
            BindableProperty.Create("Checked", typeof(bool), typeof(ToggleButton), false, BindingMode.TwoWay,
                null, propertyChanged: OnCheckedChanged);

        public static readonly BindableProperty AnimateProperty =
            BindableProperty.Create("Animate", typeof(bool), typeof(ToggleButton), false);

        public static readonly BindableProperty CheckedImageProperty =
            BindableProperty.Create("CheckedImage", typeof(ImageSource), typeof(ToggleButton), null);

        public static readonly BindableProperty UnCheckedImageProperty =
            BindableProperty.Create("UnCheckedImage", typeof(ImageSource), typeof(ToggleButton), null);

        ICommand _toggleCommand;
        Image _toggleImage;

        public ToggleButton()
        {
            Initialize();
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public bool Checked
        {
            get => (bool)GetValue(CheckedProperty);
            set => SetValue(CheckedProperty, value);
        }

        public bool Animate
        {
            get => (bool)GetValue(AnimateProperty);
            set => SetValue(CheckedProperty, value);
        }

        public ImageSource CheckedImage
        {
            get => (ImageSource)GetValue(CheckedImageProperty);
            set => SetValue(CheckedImageProperty, value);
        }

        public ImageSource UnCheckedImage
        {
            get => (ImageSource)GetValue(UnCheckedImageProperty);
            set => SetValue(UnCheckedImageProperty, value);
        }

        public ICommand ToogleCommand
        {
            get
            {
                return _toggleCommand ?? (_toggleCommand = new Command(() =>
                {
                           Checked = !Checked;
                           Command?.Execute(CommandParameter);
                }));
            }
        }

        void Initialize()
        {
            _toggleImage = new Image();

            Animate = true;

            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = ToogleCommand
            });

            _toggleImage.Source = UnCheckedImage;
            Content = _toggleImage;
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            _toggleImage.Source = UnCheckedImage;
            Content = _toggleImage;
        }

        static async void OnCheckedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var toggleButton = (ToggleButton)bindable;

            if (Equals(newValue, null) && !Equals(oldValue, null))
                return;

            toggleButton._toggleImage.Source = toggleButton.Checked ? toggleButton.CheckedImage : toggleButton.UnCheckedImage;

            toggleButton.Content = toggleButton._toggleImage;

            if (!toggleButton.Animate)
                return;
            await toggleButton.ScaleTo(0.9, 50, Easing.Linear);
            await Task.Delay(100);
            await toggleButton.ScaleTo(1, 50, Easing.Linear);
        }
    }
}