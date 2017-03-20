using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace eoTouchDelivery.Core.ViewModels
{
	public class ViewModelBase : INotifyPropertyChanged
	{
		string _title = string.Empty;
		bool _isBusy;

		public const string IsBusyPropertyName = "IsBusy";

		public bool IsBusy
		{
			get => _isBusy;
			set => SetPropertyValue(ref _isBusy, value, IsBusyPropertyName);
		}

		void SetPropertyValue(ref bool isBusy, bool value, string isBusyPropertyName)
		{
		}

		public string Title
		{
			get => _title;
			set => SetPropertyValue(ref _title, value);
		}

		private object SetPropertyValue(ref string title, string value) => throw new NotImplementedException();

		public bool IsInitialized { get; set; }
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

