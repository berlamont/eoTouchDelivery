using System.Threading.Tasks;

namespace eoTouchDelivery.Core.Services
{
	public interface IDialogService
	{
		Task ShowAlertAsync(string message, string title, string buttonLabel);
	}
}
