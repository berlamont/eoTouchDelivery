using eoTouchDelivery.Core.Interfaces;
using eoTouchDelivery.Core.Services;
using eoTouchDelivery.ViewModels;
using eoTouchDelivery.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace eoTouchDelivery
{
	public partial class App : Application
	{
        public App()
		{
			InitializeComponent();

			var masterDetailPage = new MasterDetailPage
			{
				Title = "eoTouch Delivery",
				Master = new MenuPage(),
				Detail = new HomePage()
			};

			Current.MainPage = masterDetailPage;


			var ds = Core.Services.DependencyService.Init();
			ds.Register<BaseViewModel>();

			var ns = ds.Get<FormsNavigationPageService>();
			ns.RegisterPage(AppPages.ItemsPage, () => new ItemsPage());
			//ns.RegisterPage(AppPages.ItemDetailPage, new ItemDetailPage());
			ns.RegisterPage(AppPages.MenuPage, () => new MenuPage());
			ns.RegisterPage(AppPages.HomePage, () => {
				masterDetailPage.IsPresented = true;
				return null;
			});

		}
	}

	enum AppPages
	{
		HomePage,
		MenuPage,
		ItemsPage,
		ItemDetailPage,
		CustomersPage,
		CustomerDetailPage,
	}
}
