namespace AppCenter.Wpf
{
    using System.Collections.Generic;
    using System.Windows;
    using Microsoft.AppCenter.Analytics;

    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Ctor

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void TestButton_OnClick(object sender, RoutedEventArgs e)
        {
            Analytics.TrackEvent("Example");
        }

        private void MusicEventButton_OnClick(object sender, RoutedEventArgs e)
        {
            Analytics.TrackEvent("Example", new Dictionary<string, string> {{"Category", "Music"}});
        }

        #endregion
    }
}