using System;
using eoTouchDelivery.Core.Animations;
using Xamarin.Forms;

namespace eoTouchDelivery.Core.Extensions
{
    public static class AnimationExtension
    {
		public static async void Animate(this VisualElement visualElement, AnimationBase animation, Action onFinishedCallback = null)
		{
		    animation.Target = visualElement;

		    await animation.Begin();

		    onFinishedCallback?.Invoke();
		}
	}
}
