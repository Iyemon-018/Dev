﻿namespace AppCenter.Wpf
{
    using System.Windows;
    using Microsoft.AppCenter;
    using Microsoft.AppCenter.Analytics;
    using Microsoft.AppCenter.Crashes;

    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        #region Methods

        /// <summary>
        /// <see cref="E:System.Windows.Application.Startup" /> イベントを発生させます。
        /// </summary>
        /// <param name="e">
        /// イベント データを格納している <see cref="T:System.Windows.StartupEventArgs" />。
        /// </param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

#if DEBUG
            Analytics.SetEnabledAsync(false);
#else
            Analytics.SetEnabledAsync(true);
#endif

            AppCenter.Start("00289a3c-e780-405d-b78c-4fecdef2269c", typeof(Analytics), typeof(Crashes));
            AppCenterAnalytics.Initialize();
            AppCenterAnalytics.SetCountryCode();
        }

        #endregion
    }
}