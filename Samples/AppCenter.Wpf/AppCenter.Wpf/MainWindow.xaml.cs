namespace AppCenter.Wpf
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using Microsoft.AppCenter.Analytics;
    using Microsoft.AppCenter.Crashes;

    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TestButton_OnClick(object sender, RoutedEventArgs e)
        {
            Analytics.TrackEvent("Example");
        }

        private void MusicEventButton_OnClick(object sender, RoutedEventArgs e)
        {
            Analytics.TrackEvent("Example", new Dictionary<string, string> {{"Category", "Music"}});
        }

        private void CrashButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new ApplicationException($"これは手動で発生さました。({DateTime.Now:yyyy-MM-dd HH:mm:ss})");
        }

        private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            bool didAppCrash = await Crashes.HasCrashedInLastSessionAsync();
            if (didAppCrash)
            {
                ErrorReport crashReport = await Crashes.GetLastSessionCrashReportAsync();
                MessageBox.Show($"前回セッションでクラッシュしました。{Environment.NewLine}"
                                + $"- 発生日時 : {crashReport.AppErrorTime}{Environment.NewLine}"
                                + $"- エラー内容 : {crashReport.Exception.Message}"
                              , "Report"
                              , MessageBoxButton.OK
                              , MessageBoxImage.Information);
            }
        }
    }
}