// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace eoTouchDelivery.Core.Utils
{
  /// <summary>
  /// This is the Settings static class that can be used in your Core solution or in any
  /// of your client applications. All settings are laid out the same exact way with getters
  /// and setters. 
  /// </summary>
  public static class Settings
  {
    private static ISettings AppSettings
    {
      get
      {
        return CrossSettings.Current;
      }
    }

    #region Setting Constants

    private const string SettingsKey = "settings_key";
    private static readonly string SettingsDefault = string.Empty;
    private const string UserIdKey = "user_id_key";
    private static readonly int UserIdDefault = 0;
    private const string UwpWindowSizeKey = "uwp_window_size";
    private static readonly string UwpWindowSizeDefault = string.Empty;

    #endregion


    public static string GeneralSettings
    {
      get
      {
        return AppSettings.GetValueOrDefault<string>(SettingsKey, SettingsDefault);
      }
      set
      {
        AppSettings.AddOrUpdateValue<string>(SettingsKey, value);
      }
    }

    public static int UserId
    {
      get
      {
        return AppSettings.GetValueOrDefault(UserIdKey, UserIdDefault);
      }
      set
      {
        AppSettings.AddOrUpdateValue(UserIdKey, value);
      }
    }
    public static string UwpWindowSize
    {
      get
      {
        return AppSettings.GetValueOrDefault(UwpWindowSizeKey, UwpWindowSizeDefault);
      }
      set
      {
        AppSettings.AddOrUpdateValue(UwpWindowSizeKey, value);
      }
    }

    public static int ProfileId { get; set; }
    public static string AccessToken { get; set; }
    public static int SupportBit { get; set; }

    public static void RemoveUserId()
    {
    }

    public static void RemoveProfileId()
    {
    }

    public static void RemoveAccessToken()
    {
    }
  }
}