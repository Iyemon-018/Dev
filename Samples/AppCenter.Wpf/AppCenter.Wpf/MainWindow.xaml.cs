namespace AppCenter.Wpf
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows;
    using Microsoft.AppCenter.Analytics;
    using Microsoft.AppCenter.Crashes;
    using Microsoft.Win32;

    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var version = Assembly.GetEntryAssembly().GetName().Version;
            Title = $"{Title} - Ver.{version}";
        }

        private void TestButton_OnClick(object sender, RoutedEventArgs e)
        {
            Analytics.TrackEvent("Example", new Dictionary<string, string>{{"Test", "TestButton"}});
        }

        private void MusicEventButton_OnClick(object sender, RoutedEventArgs e)
        {
            Analytics.TrackEvent("Example", new Dictionary<string, string> {{"Category", "Music"}});
        }

        private void CrashButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new ApplicationException($"これは手動で発生させました。({DateTime.Now:yyyy-MM-dd HH:mm:ss})");
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

        private void ExceptionButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                throw new ApplicationException($"これは手動で発生させました。- Exception ({DateTime.Now:yyyy-MM-dd HH:mm:ss})");
            }
            catch (ApplicationException exception)
            {
                var properties = new Dictionary<string, string>
                                 {
                                     { "Category", "Test" },
                                 };
                Microsoft.AppCenter.Crashes.Crashes.TrackError(exception, properties);
            }
        }

        private void FileSelectButton_OnClick(object          sender
                                            , RoutedEventArgs e)
        {
            Analytics.TrackEvent("Example", new Dictionary<string, string> { { "File Select", "Click" } });

            var dialog = new OpenFileDialog {InitialDirectory = Environment.CurrentDirectory};
            if (dialog.ShowDialog(this).GetValueOrDefault(false))
            {
                Analytics.TrackEvent("Example", new Dictionary<string, string> { { "File Select", dialog.FileName } });
            }
        }

        private void SendMessageButton_OnClick(object          sender
                                             , RoutedEventArgs e)
        {
            Analytics.TrackEvent("Example", new Dictionary<string, string> { { "メッセージ送信", "メッセージ送信ボタンが押された。" } });
        }
    }
}