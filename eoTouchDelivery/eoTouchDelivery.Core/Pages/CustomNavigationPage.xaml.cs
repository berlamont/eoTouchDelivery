﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

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
