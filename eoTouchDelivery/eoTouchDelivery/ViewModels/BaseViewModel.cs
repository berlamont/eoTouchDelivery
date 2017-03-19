using eoTouchDelivery.Core.Mvvm;

namespace eoTouchDelivery.ViewModels
{
	public class BaseViewModel : SimpleViewModel
	{
		string _title = string.Empty;
		bool _isBusy;

		public const string IsBusyPropertyName = "IsBusy";

		public bool IsBusy
		{
			get { return _isBusy; }
			set
			{ SetPropertyValue(ref _isBusy, value, IsBusyPropertyName); }
		}

		public string Title
		{
			get { return _title; }
			set { SetPropertyValue(ref _title, value); }
		}

		public bool IsInitialized { get; set; }
	}
}

