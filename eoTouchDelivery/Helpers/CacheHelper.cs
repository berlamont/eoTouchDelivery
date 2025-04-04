using System.Threading.Tasks;
using FFImageLoading.Cache;
using FFImageLoading.Forms;

namespace eoTouchDelivery.Core.Helpers
{
    public class CacheHelper
    {
        public static async Task RemoveFromCache(string url)
        {
            await CachedImage.InvalidateCache(url, CacheType.All, true);
        }
    }
}
