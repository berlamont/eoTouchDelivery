using System.Linq;
using System.Threading.Tasks;
using eoTouchDelivery.Core.Helpers;
using eoTouchDelivery.Core.Services;
using eoTouchDelivery.Core.ViewModels.Base;
using Xamarin.Forms;

namespace eoTouchDelivery.Core
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			AdaptColorsToHexString();

			if (Device.OS == TargetPlatform.Windows)
			{
				InitNavigation();
			}
		}

		protected override async void OnStart()
		{
			base.OnStart();

			if (Device.OS != TargetPlatform.Windows)
			{
				await InitNavigation();
			}
		}

		Task InitNavigation()
		{
			var navigationService = ViewModelLocator.Instance.Resolve<INavigationService>();
			return navigationService.InitializeAsync();
		}

		void AdaptColorsToHexString()
		{
			for (var i = 0;i < this.Resources.Count;i++)
			{
				var key = this.Resources.Keys.ElementAt(i);
				var resource = this.Resources[key];

				if (!(resource is Color))
					continue;
				var color = (Color)resource;
				Resources.Add(key + "HexString", color.ToHexString());
			}
		}
	}
}
