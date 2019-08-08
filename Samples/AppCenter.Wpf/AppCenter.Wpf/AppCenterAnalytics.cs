namespace AppCenter.Wpf
{
    using System.Collections.Generic;
    using System.Globalization;
    using Microsoft.AppCenter;
    using Microsoft.AppCenter.Analytics;

    public class AppCenterAnalytics
    {
        public static void SetCountryCode()
        {
            // This fallback country code does not reflect the physical device location, but rather the
            // country that corresponds to the culture it uses.
            var countryCode = RegionInfo.CurrentRegion.TwoLetterISORegionName;
            AppCenter.SetCountryCode(countryCode);
        }

        public static void Initialize()
        {
            Analytics.TrackEvent("Example", new Dictionary<string, string>
                                            {
                                                {"Category", "Music"}, {"FileName", "favorite.avi"}
                                            });
        }
    }
}