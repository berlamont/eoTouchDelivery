using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
