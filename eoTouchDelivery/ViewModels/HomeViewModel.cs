using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace eoTouchDelivery.Core.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
	    public HomeViewModel()
	    {
		    
	    }


	    public override async Task InitializeAsync(object navigationData)
	    {
		    IsBusy = true;
		    IsBusy = true;

		    try
		    {
			  
		    } catch (Exception ex) when (ex is WebException || ex is HttpRequestException)
		    {
			    await DialogService.ShowAlertAsync("Communication error", "Error", "Ok");
		    } catch (Exception ex)
		    {
			    Debug.WriteLine($"Error loading data in: {ex}");
		    }

		    IsBusy = false;
	    }
	}
}
