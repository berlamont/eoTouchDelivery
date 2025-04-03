using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace eoTouchDelivery.Core.Helpers
{
    public static class ColorHelper
    {
        public static string ToHexString(this Color c) => $"#{ColorAsInt(c.Alpha):x2}{ColorAsInt(c.Red):x2}{ColorAsInt(c.Green):x2}{ColorAsInt(c.Blue):x2}";

        static int ColorAsInt(double color) => (int)(color * 255);
    }
}
