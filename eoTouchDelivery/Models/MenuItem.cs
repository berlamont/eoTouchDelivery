using System;
using eoTouchDelivery.Core.ViewModels.Base;
using eoTouchDelivery.Core.Models.Enums;

namespace eoTouchDelivery.Core.Models
{
	public class MenuItem : ExtendedBindableObject
	{
		bool _isEnabled;

		MenuItemType _menuItemType;
		string _title;

		Type _viewModelType;

		public string Title
		{
			get => _title;

			set
			{
				_title = value;
				RaisePropertyChanged(() => Title);
			}
		}

		public MenuItemType MenuItemType
		{
			get => _menuItemType;

			set
			{
				_menuItemType = value;
				RaisePropertyChanged(() => MenuItemType);
			}
		}

		public Type ViewModelType
		{
			get => _viewModelType;

			set
			{
				_viewModelType = value;
				RaisePropertyChanged(() => ViewModelType);
			}
		}

		public bool IsEnabled
		{
			get => _isEnabled;

			set
			{
				_isEnabled = value;
				RaisePropertyChanged(() => IsEnabled);
			}
		}
	}
}