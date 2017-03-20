using Xamarin.Forms;

namespace eoTouchDelivery.Core.Behaviors
{
	/// <summary>
	///     This behavior adds an Effect instance to a control when the behavior is attached to the control,
	///     and removes the Effect instance when the behavior is detached from the control.
	/// </summary>
	/// <example>
	///     <!--[CDATA[
	///  <Label Text = "Label Shadow Effect">
	///  	<Label.Behaviors>
	/// 			<inf:EffectBehavior Group="Xamarin" Name="LabelShadowEffect" />
	///  	</Label.Behaviors>
	///  </Label>
	///  ]]>-->
	/// </example>
	public class EffectBehavior : Behavior<View>
	{
		/// <summary>
		///     Bindable property for the ResolutionGroupName attribute of the Effect.
		/// </summary>
		public static readonly BindableProperty GroupProperty = BindableProperty.Create("Group", typeof(string), typeof(EffectBehavior), null);

		/// <summary>
		///     Bindable property for the ExportEffect attribute of the Effect.
		/// </summary>
		public static readonly BindableProperty NameProperty = BindableProperty.Create("Name", typeof(string), typeof(EffectBehavior), null);

		/// <summary>
		///     The group name of the Effect
		/// </summary>
		/// <value>The ResolutionGroupName value of the Effect.</value>
		public string Group
		{
			get => (string) GetValue(GroupProperty);
			set => SetValue(GroupProperty, value);
		}

		/// <summary>
		///     The name of the Effect.
		/// </summary>
		/// <value>The ExportEffect value of the Effect.</value>
		public string Name
		{
			get => (string) GetValue(NameProperty);
			set => SetValue(NameProperty, value);
		}

		/// <summary>
		///     Called when the behavior is attached to an element.
		/// </summary>
		/// <param name="bindable">The attached object.</param>
		protected override void OnAttachedTo(BindableObject bindable)
		{
			base.OnAttachedTo(bindable);
			AddEffect(bindable as View);
		}

		/// <summary>
		///     Called when the behavior is being removed from an element.
		/// </summary>
		/// <param name="bindable">The object being detached from.</param>
		protected override void OnDetachingFrom(BindableObject bindable)
		{
			RemoveEffect(bindable as View);
			base.OnDetachingFrom(bindable);
		}

		/// <summary>
		///     Adds the Effect to the element's Effects collection.
		/// </summary>
		/// <param name="view">The View to add the Effect to.</param>
		void AddEffect(View view)
		{
			var effect = GetEffect();
			if (effect != null)
				view.Effects.Add(GetEffect());
		}

		/// <summary>
		///     Removes the Effect from the element's Effects collection.
		/// </summary>
		/// <param name="view">The View to remove the Effect from.</param>
		void RemoveEffect(View view)
		{
			var effect = GetEffect();
			if (effect != null)
				view.Effects.Remove(GetEffect());
		}

		/// <summary>
		///     Resolves the Effect to be added to an element.
		/// </summary>
		/// <returns>The resolved Effect.</returns>
		Effect GetEffect()
		{
			if (!string.IsNullOrWhiteSpace(Group) && !string.IsNullOrWhiteSpace(Name))
				return Effect.Resolve($"{Group}.{Name}");
			return null;
		}
	}
}