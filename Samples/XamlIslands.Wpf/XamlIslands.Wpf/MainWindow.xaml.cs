namespace XamlIslands.Wpf
{
    using System.Windows;
    using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;

    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void MapControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            BasicGeoposition cityPosition = new BasicGeoposition { Latitude = 34.702476, Longitude = 135.495946};
            Geopoint         cityCenter   = new Geopoint(cityPosition);
            bool result = await MapControl.TrySetViewAsync(center:cityCenter, zoomLevel:14);
        }
    }
}