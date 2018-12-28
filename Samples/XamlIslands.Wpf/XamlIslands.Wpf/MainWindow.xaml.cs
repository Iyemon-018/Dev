namespace XamlIslands.Wpf
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;

    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isLoadedMap;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void MapControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            BasicGeoposition cityPosition = new BasicGeoposition {Latitude = 34.702476, Longitude = 135.495946};
            Geopoint         cityCenter   = new Geopoint(cityPosition);

            while (!_isLoadedMap)
            {
                // animation が設定されていると座標移動に時間がかかる。
                // 初期座標を設定するならMapAnimationKind.None がよさげ。
                bool result = await MapControl.TrySetViewAsync(center: cityCenter, zoomLevel: 14, heading: 0
                                                             , desiredPitch: 0, animation: MapAnimationKind.None);
                if (!result)
                {
                    // １回では初期座標の設定が失敗する可能性が高い。
                    // 成功するまで間隔おいて再チャレンジする。
                    await Task.Delay(TimeSpan.FromMilliseconds(500));
                    continue;
                }

                _isLoadedMap = true;
            }

            ZoomSlider.Value = 14;
        }

        private async void ZoomSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            await MapControl.TryZoomToAsync((double) (sender as Slider)?.Value);
        }

        private async void RotateSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!_isLoadedMap)
            {
                return;
            }

            await MapControl.TryRotateToAsync(RotateSlider.Value);
        }

        private async void TiltSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!_isLoadedMap)
            {
                return;
            }

            await MapControl.TryTiltToAsync(TiltSlider.Value);
        }
    }
}