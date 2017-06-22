using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VisualTreeHelperSample
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly List<DependencyObject> _hitResults = new List<DependencyObject>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TouchElementViewListBox.ItemsSource = null;
            _hitResults.Clear();

            Point position = e.GetPosition(this);
            VisualTreeHelper.HitTest(this
                                     , new HitTestFilterCallback(OnHitTestFilterCallback)
                                     , new HitTestResultCallback(OnHitTestResultCallback)
                                     , new PointHitTestParameters(position));

            TouchElementViewListBox.ItemsSource = _hitResults.OfType<FrameworkElement>()
                                                             .Select(x => $"{x.Name}:{x}")
                                                             .ToList();
        }

        private HitTestFilterBehavior OnHitTestFilterCallback(DependencyObject target)
        {
            UIElement element = target as UIElement;
            if (element != null)
            {
                if (element.Visibility != Visibility.Visible
                    || element.Opacity <= 0)
                {
                    return HitTestFilterBehavior.ContinueSkipSelfAndChildren; 
                }
            }
            return HitTestFilterBehavior.Continue;
        }

        private HitTestResultBehavior OnHitTestResultCallback(HitTestResult result)
        {
            _hitResults.Add(result.VisualHit);
            return HitTestResultBehavior.Continue;
        }

        private void HiddenPanelBCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            PanelB.Visibility = Visibility.Collapsed;
        }

        private void HiddenPanelBCheckBox_OnUnchecked(object sender, RoutedEventArgs e)
        {
            PanelB.Visibility = Visibility.Visible;
        }
    }
}