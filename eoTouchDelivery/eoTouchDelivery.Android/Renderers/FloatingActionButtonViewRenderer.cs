using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using com.refractored.fab;
using eoTouchDelivery.Core.Controls;
using eoTouchDelivery.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ListView = Xamarin.Forms.ListView;
using Path = System.IO.Path;

[assembly: ExportRenderer(typeof(FloatingActionButtonView), typeof(FloatingActionButtonViewRenderer))]
namespace eoTouchDelivery.Droid.Renderers
{
    public class FloatingActionButtonViewRenderer : ViewRenderer<FloatingActionButtonView, FrameLayout>
    {
        const int MARGIN_DIPS = 16;
        const int FAB_HEIGHT_NORMAL = 56;
        const int FAB_HEIGHT_MINI = 40;
        const int FAB_FRAME_HEIGHT_WITH_PADDING = MARGIN_DIPS * 2 + FAB_HEIGHT_NORMAL;
        const int FAB_FRAME_WIDTH_WITH_PADDING = MARGIN_DIPS * 2 + FAB_HEIGHT_NORMAL;
        const int FAB_MINI_FRAME_HEIGHT_WITH_PADDING = MARGIN_DIPS * 2 + FAB_HEIGHT_MINI;
        const int FAB_MINI_FRAME_WIDTH_WITH_PADDING = MARGIN_DIPS * 2 + FAB_HEIGHT_MINI;
        readonly Context context;
        readonly FloatingActionButton fab;
        int appearingListItemIndex;

        public FloatingActionButtonViewRenderer()
        {
            context = Forms.Context;

            var d = context.Resources.DisplayMetrics.Density;
            var margin = (int) (MARGIN_DIPS * d); // margin in pixels

            fab = new FloatingActionButton(context);
            var lp = new FrameLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent)
            {
                Gravity = GravityFlags.CenterVertical | GravityFlags.CenterHorizontal,
                LeftMargin = margin,
                TopMargin = margin,
                BottomMargin = margin,
                RightMargin = margin
            };
            fab.LayoutParameters = lp;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<FloatingActionButtonView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
                return;

            if (e.OldElement != null)
            {
                e.OldElement.PropertyChanged -= HandlePropertyChanged;
                // Experimental - Hiding and showing the FAB correctly is dependent on the objects in the list being unique
                if (e.OldElement.ParentList != null)
                {
                    e.OldElement.ParentList.ItemAppearing -= OnListItemAppearing;
                    e.OldElement.ParentList.ItemDisappearing -= OnListItemDisappearing;
                }
            }

            if (Element != null)
            {
                Element.PropertyChanged += HandlePropertyChanged;
                // Experimental - Hiding and showing the FAB correctly is dependent on the objects in the list being unique
                if (Element.ParentList != null)
                {
                    Element.ParentList.ItemAppearing += OnListItemAppearing;
                    Element.ParentList.ItemDisappearing += OnListItemDisappearing;
                }
            }

            Element.Show = Show;
            Element.Hide = Hide;

            SetFabImage(Element.ImageName);
            SetFabSize(Element.Size);

            fab.ColorNormal = Element.ColorNormal.ToAndroid();
            fab.ColorPressed = Element.ColorPressed.ToAndroid();
            fab.ColorRipple = Element.ColorRipple.ToAndroid();
            fab.HasShadow = Element.HasShadow;
            fab.Click += Fab_Click;

            var frame = new FrameLayout(context);
            frame.RemoveAllViews();
            frame.AddView(fab);

            SetNativeControl(frame);
        }

        async void OnListItemDisappearing(object sender, ItemVisibilityEventArgs e)
        {
            // Experimental - Hiding and showing the FAB correctly is dependent on the objects in the list being unique
            var list = sender as ListView;
            if (list == null)
                return;
            await Task.Run(() =>
            {
                var items = list.ItemsSource as IList;
                if (items == null)
                    return;
                var index = items.IndexOf(e.Item);
                if (index < appearingListItemIndex && index >= 0)
                {
                    appearingListItemIndex = index;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        fab.Hide();
                        await Task.Delay(500);
                        fab.Show();
                    });
                }
                else
                {
                    appearingListItemIndex = index;
                }
            });
        }

        async void OnListItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            // Experimental - Hiding and showing the FAB correctly is dependent on the objects in the list being unique
            var list = sender as ListView;
            if (list == null)
                return;
            await Task.Run(() =>
            {
                var items = list.ItemsSource as IList;
                if (items == null)
                    return;
                var index = items.IndexOf(e.Item);
                if (index < appearingListItemIndex)
                {
                    appearingListItemIndex = index;
                    Device.BeginInvokeOnMainThread(() => fab.Show());
                }
                else
                {
                    appearingListItemIndex = index;
                }
            });
        }

        public void Show(bool animate = true)
        {
            fab.Show(animate);
        }

        public void Hide(bool animate = true)
        {
            fab.Hide(animate);
        }

        void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Content")
                Tracker.UpdateLayout();
            else if (e.PropertyName == FloatingActionButtonView.ColorNormalProperty.PropertyName)
                fab.ColorNormal = Element.ColorNormal.ToAndroid();
            else if (e.PropertyName == FloatingActionButtonView.ColorPressedProperty.PropertyName)
                fab.ColorPressed = Element.ColorPressed.ToAndroid();
            else if (e.PropertyName == FloatingActionButtonView.ColorRippleProperty.PropertyName)
                fab.ColorRipple = Element.ColorRipple.ToAndroid();
            else if (e.PropertyName == FloatingActionButtonView.ImageNameProperty.PropertyName)
                SetFabImage(Element.ImageName);
            else if (e.PropertyName == FloatingActionButtonView.SizeProperty.PropertyName)
                SetFabSize(Element.Size);
            else if (e.PropertyName == FloatingActionButtonView.HasShadowProperty.PropertyName)
                fab.HasShadow = Element.HasShadow;
        }

        void SetFabImage(string imageName)
        {
            if (!string.IsNullOrWhiteSpace(imageName))
                try
                {
                    var drawableNameWithoutExtension = Path.GetFileNameWithoutExtension(imageName);
                    var resources = context.Resources;
                    var imageResourceName = resources.GetIdentifier(drawableNameWithoutExtension, "drawable",
                        context.PackageName);
                    fab.SetImageBitmap(BitmapFactory.DecodeResource(context.Resources, imageResourceName));
                }
                catch (Exception ex)
                {
                    throw new FileNotFoundException("There was no Android Drawable by that name.", ex);
                }
        }

        void SetFabSize(FloatingActionButtonSize size)
        {
            if (size == FloatingActionButtonSize.Mini)
            {
                fab.Size = FabSize.Mini;
                Element.WidthRequest = FAB_MINI_FRAME_WIDTH_WITH_PADDING;
                Element.HeightRequest = FAB_MINI_FRAME_HEIGHT_WITH_PADDING;
            }
            else
            {
                fab.Size = FabSize.Normal;
                Element.WidthRequest = FAB_FRAME_WIDTH_WITH_PADDING;
                Element.HeightRequest = FAB_FRAME_HEIGHT_WITH_PADDING;
            }
        }

        void Fab_Click(object sender, EventArgs e)
        {
            var clicked = Element.Clicked;
            if (Element == null)
                return;
            clicked?.Invoke(sender, e);
            if (Element.Command != null && Element.Command.CanExecute(null))
                Element.Command.Execute(null);
        }

        protected override FrameLayout CreateNativeControl() => null;
    }
}