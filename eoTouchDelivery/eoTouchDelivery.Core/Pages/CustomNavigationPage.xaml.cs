﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eoTouchDelivery.Core.Pages
{
	public partial class CustomNavigationPage : NavigationPage
	{
		public CustomNavigationPage() : base()
		{
			InitializeComponent();
		}

		public CustomNavigationPage(Page root) : base(root)
		{
			InitializeComponent();
		}
	}
}
