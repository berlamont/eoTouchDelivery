using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eoTouchDelivery.Core.Models.Enums;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace eoTouchDelivery.Core.Converters
{
    /// <summary>
    /// Images for menu
    /// </summary>
    public class MenuItemTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var menuItemType = (MenuItemType)value;

            // TODO Xamarin.Forms.Device.OS is no longer supported. Use Microsoft.Maui.Devices.DeviceInfo.Platform instead. For more details see https://learn.microsoft.com/en-us/dotnet/maui/migration/forms-projects#device-changes
            var platform = Device.OS == TargetPlatform.Windows;

            switch (menuItemType)
            {
               case MenuItemType.Home:
                    return platform ? "Assets/uwp_ic_home.png" : string.Empty;
                case MenuItemType.StartDayTruckInspection:
                    return platform ? "Assets/uwp_ic_home.png" : string.Empty;
                case MenuItemType.CheckOut:
                    return platform ? "Assets/uwp_ic_home.png" : string.Empty;
                case MenuItemType.Route:
                    return platform ? "Assets/uwp_ic_home.png" : string.Empty;
                case MenuItemType.TruckInventory:
                    return platform ? "Assets/uwp_ic_home.png" : string.Empty;
                case MenuItemType.IONReports:
                    return platform ? "Assets/uwp_ic_home.png" : string.Empty;
                case MenuItemType.EndDayTruckInspection:
                    return platform ? "Assets/uwp_ic_home.png" : string.Empty;
                case MenuItemType.CheckIn:
                    return platform ? "Assets/uwp_ic_home.png" : string.Empty;
                case MenuItemType.Reports:
                    return platform ? "Assets/uwp_ic_home.png" : string.Empty;
                case MenuItemType.Done:
                    return platform ? "Assets/uwp_ic_home.png" : string.Empty;
                case MenuItemType.Settings:
                    return platform ? "Assets/uwp_ic_home.png" : string.Empty;
                case MenuItemType.Sync:
                    return platform ? "Assets/uwp_ic_home.png" : string.Empty;
                default:
                    return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
