using System.Threading.Tasks;
using eoTouchDelivery.Interfaces;
using Xamarin.Forms;

namespace eoTouchDelivery.Infrastructure.Services
{
    /// <summary>
    /// Wrapper around Page.DisplayAlert to turn it into a Message Visualizer 
    /// service for Xamarin.Forms which can be used from a ViewModel and mocked
    /// for Unit Testing.
    /// </summary>
    public class FormsMessageVisualizerService : IMessageVisualizerService
    {
        /// <summary>
        /// Show a message using the Forms DisplayAlert method.
        /// </summary>
        /// <returns>The message.</returns>
        /// <param name="title">Title.</param>
        /// <param name="message">Message.</param>
        /// <param name="ok">Ok.</param>
        /// <param name="cancel">Cancel.</param>
        public async Task<bool> ShowMessage(
            string title, string message, string ok, string cancel=null)
        {
            if (cancel == null) {
                await Application.Current.MainPage.DisplayAlert(title, message, ok);
                return true;
            }

            return await Application.Current.MainPage.DisplayAlert(
                title, message, ok, cancel);
        }
    }
}

