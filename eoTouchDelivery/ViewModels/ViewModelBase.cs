using System.Threading.Tasks;
using eoTouchDelivery.Core.Services;
using eoTouchDelivery.Core.ViewModels.Base;

namespace eoTouchDelivery.Core.ViewModels
{
	public abstract class ViewModelBase : ExtendedBindableObject
	{
		protected readonly IDialogService DialogService;
		protected readonly INavigationService NavigationService;

		bool _isBusy;

		public bool IsBusy
		{
			get
			{
				return _isBusy;
			}

			set
			{
				_isBusy = value;
				RaisePropertyChanged(() => IsBusy);
			}
		}

		public ViewModelBase()
		{
			DialogService = ViewModelLocator.Instance.Resolve<IDialogService>();
			NavigationService = ViewModelLocator.Instance.Resolve<INavigationService>();
		}

		public virtual Task InitializeAsync(object navigationData)
		{
			return Task.FromResult(false);
		}
	}
}
