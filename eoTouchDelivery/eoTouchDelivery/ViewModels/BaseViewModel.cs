using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using eoTouchDelivery.Helpers;
using eoTouchDelivery.Models;
using eoTouchDelivery.Services;

using Xamarin.Forms;

namespace eoTouchDelivery.ViewModels
{
	public class BaseViewModel : ObservableObject
	{
		/// <summary>
		/// Get the azure service instance
		/// </summary>
		public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

		bool isBusy;
		public bool IsBusy
		{
			get { return isBusy; }
			set { SetProperty(ref isBusy, value); }
		}
		/// <summary>
		/// Private backing field to hold the title
		/// </summary>
		string title = string.Empty;
		/// <summary>
		/// Public property to set and get the title of the item
		/// </summary>
		public string Title
		{
			get { return title; }
			set { SetProperty(ref title, value); }
		}

		public event PropertyChangedEventHandler PropertyChanged = delegate { };

		protected void RaiseAllPropertiesChanged()
		{
			//empty string makes all properties invalid
			PropertyChanged(this, new PropertyChangedEventArgs(string.Empty));
		}

		protected void RaisePropertyChanged<T>(Expression<Func<T>> propExpr)
		{
			var prop = (PropertyInfo)((MemberExpression)propExpr.Body).Member;
			if (prop?.Name != null)
				RaisePropertyChanged(prop.Name);
		}

		protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		protected bool SetPropertyValue<T>(ref T storageField, T newValue, Expression<Func<T>> propExpr)
		{
			if (Equals(storageField, newValue))
				return false;

			storageField = newValue;
			var prop = (PropertyInfo)((MemberExpression)propExpr.Body).Member;
			if (prop?.Name != null)
				RaisePropertyChanged(prop.Name);

			return true;
		}

		protected bool SetPropertyValue<T>(ref T storageField, T newValue, [CallerMemberName] string propertyName = "")
		{
			if (Equals(storageField, newValue))
				return false;

			storageField = newValue;
			RaisePropertyChanged(propertyName);

			return true;
		}
	}
}

