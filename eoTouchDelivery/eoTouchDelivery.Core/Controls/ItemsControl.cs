using System.Collections;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace eoTouchDelivery.Core.Controls
{
    /// <summary>
    /// Simple ItemsControl to render a list of things in a stacked view using
    /// either text labels, or an inflated data template. It also includes the ability
    /// to display a text placeholder if no items are present in the data bound collection.
    /// </summary>
    public class ItemsControl : ContentView
    {
        /// <summary>
        /// Bindable property for the placeholder text.
        /// </summary>
        public static readonly BindableProperty PlaceholderTextProperty = BindableProperty.Create (
            "PlaceholderText", typeof (string), typeof (ItemsControl));

        /// <summary>
        /// Gets or sets the placeholder text.
        /// </summary>
        /// <value>The placeholder text.</value>
        public string PlaceholderText {
            get => (string)GetValue (PlaceholderTextProperty);
            set => SetValue (PlaceholderTextProperty, value);
        }

        /// <summary>
        /// Bindable property for the Label style used for each item when there
        /// is no data template assigned.
        /// </summary>
        public static readonly BindableProperty ItemStyleProperty = BindableProperty.Create (
            "ItemStyle", typeof (Style), typeof (ItemsControl), propertyChanged: OnItemStylePropertyChanged);

        /// <summary>
        /// Gets or sets the item style used for dynamically generated labels.
        /// </summary>
        /// <value>The item style.</value>
        public Style ItemStyle {
            get => (Style)GetValue (ItemStyleProperty);
            set => SetValue (ItemStyleProperty, value);
        }

        /// <summary>
        /// Bindable property for the data source
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create (
            "ItemsSource", typeof (IList), typeof (ItemsControl), propertyChanging: OnItemsSourceChanged);

        /// <summary>
        /// Gets or sets the items source - can be any collection of elements.
        /// </summary>
        /// <value>The items source.</value>
        public IList ItemsSource {
            get => (IList)GetValue (ItemsSourceProperty);
            set => SetValue (ItemsSourceProperty, value);
        }

        /// <summary>
        /// Bindable property for the data template to visually represent each item.
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create (
            "ItemTemplate", typeof (DataTemplate), typeof (ItemsControl));

        /// <summary>
        /// Gets or sets the item template used to generate the visuals for a single item.
        /// </summary>
        /// <value>The item template.</value>
        public DataTemplate ItemTemplate {
            get => (DataTemplate)GetValue (ItemTemplateProperty);
            set => SetValue (ItemTemplateProperty, value);
        }

        // Data
        StackLayout _stack;
        Label _noItemsLabel;

        /// <summary>
        /// Initializes an ItemsControl.
        /// </summary>
        public ItemsControl ()
        {
            Padding = new Thickness(5,0,5,5);

            _stack = new StackLayout {
                Spacing = 10,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            _noItemsLabel = new Label {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };
            _noItemsLabel.SetBinding (StyleProperty, new Binding(nameof(ItemStyle), source: this));
            _noItemsLabel.SetBinding (Label.TextProperty, new Binding (nameof (PlaceholderText), source: this));

            Content = _noItemsLabel;
        }

        /// <summary>
        /// This is called when the underlying data source is changed.
        /// </summary>
        /// <param name="bindable">ItemsSource</param>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        static void OnItemsSourceChanged (BindableObject bindable, object oldValue, object newValue)
        {
            ((ItemsControl)bindable).OnItemsSourceChangedImpl ((IList)oldValue, (IList)newValue);
        }

        /// <summary>
        /// Instance method called when the underlying data source is changed through the
        /// <see cref="ItemsSource"/> property. This re-generates the list based on the 
        /// new collection.
        /// </summary>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        void OnItemsSourceChangedImpl(IList oldValue, IList newValue)
        {
            // Unsubscribe from the old collection
            if (oldValue != null) {
                var ncc = oldValue as INotifyCollectionChanged;
                if (ncc != null)
                    ncc.CollectionChanged -= OnCollectionChanged;
            }

            if (newValue == null) {
                _stack.Children.Clear ();
                Content = _noItemsLabel;
            }
            else {
                Content = _stack;
                FillContainer (newValue);
                var ncc = newValue as INotifyCollectionChanged;
                if (ncc != null)
                    ncc.CollectionChanged += OnCollectionChanged;
            }
        }

        /// <summary>
        /// Called when the Label style is changed.
        /// </summary>
        /// <param name="bindable">ItemsControl</param>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        static void OnItemStylePropertyChanged (BindableObject bindable, object oldValue, object newValue)
        {
            ((ItemsControl)bindable).OnItemStylePropertyChangedImpl (newValue as Style);
        }

        /// <summary>
        /// Instance method called when the label style is changed through the
        /// <see cref="ItemStyle"/> property. This applies the new style to all the labels.
        /// </summary>
        /// <param name="style">Style.</param>
        void OnItemStylePropertyChangedImpl (Style style)
        {
            // Ignore if we have a data template.
            if (ItemTemplate != null)
                return;

            foreach (var view in _stack.Children)
            {
                if (!(view is Label label))
                    continue;
                if (style == null)
                    label.ClearValue(StyleProperty);
                else
                    label.Style = style;
            }
        }

        /// <summary>
        /// This method takes our items source and generates visuals for
        /// each item in the collection; it can reuse visuals which were created
        /// previously and simply changes the binding context.
        /// </summary>
        /// <param name="newValue">New items to display</param>
        void FillContainer (IList newValue)
        {
            var itemStyle = ItemStyle;
            var template = ItemTemplate;
            var visuals = _stack.Children;

            for (var i = 0; i < newValue.Count; i++) {
                var dataItem = newValue [i];

                if (visuals.Count > i)
                {
                    if (template != null)
                    {
                        var visualItem = visuals [i];
                        visualItem.BindingContext = dataItem;
                    }    
                    else
                    {
                        var visualItem = (Label) visuals [i];
                        visualItem.Text = dataItem.ToString ();
                        if (itemStyle != null) {
                            visualItem.Style = itemStyle;
                        } else {
                            visualItem.ClearValue (StyleProperty);
                        }
                    }
                }
                else
                {
                    if (template != null) {
                        InflateTemplate (template, dataItem);
                    } else {
                        var label = new Label { Text = dataItem.ToString () };
                        if (itemStyle != null) {
                            label.Style = itemStyle;
                        }
                        _stack.Children.Add (label);
                    }
                }
            }

            Content = (_stack.Children.Count == 0) ? (View) _noItemsLabel : _stack;
        }

        /// <summary>
        /// Inflates the visuals for a data template or template selector
        /// and adds it to our StackLayout.
        /// </summary>
        /// <param name="template">Template.</param>
        /// <param name="item">Item.</param>
        void InflateTemplate (DataTemplate template, object item)
        {
            // Pull real template from selector if necessary.
            if (template is DataTemplateSelector dSelector)
                template = dSelector.SelectTemplate(item, this);

            if (!(template.CreateContent() is View view))
                return;
            view.BindingContext = item;
            _stack.Children.Add(view);
        }

        /// <summary>
        /// This is called when the data source collection implements
        /// collection change notifications and the data has changed.
        /// This is not optimized - it simply replaces all the data.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void OnCollectionChanged (object sender, NotifyCollectionChangedEventArgs e) => FillContainer ((IList)sender);
    }
}

