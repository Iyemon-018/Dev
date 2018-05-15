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

namespace NotifyIconSample
{
    using System.ComponentModel;
    using Hardcodet.Wpf.TaskbarNotification;

    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel(new BalloonTipService(TaskbarIcon));
        }

        /// <summary>
        /// <see cref="TaskbarIcon"/> をダブルクリックした際に呼ばれるイベントハンドラです。
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">イベント引数オブジェクト</param>
        private void TaskbarIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            var v = new PreferencesWindow();
            v.Show();
        }

        /// <summary>
        /// <see cref="E:System.Windows.Window.Closing" /> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント データを格納している <see cref="T:System.ComponentModel.CancelEventArgs" />。</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            TaskbarIcon.Dispose();

            base.OnClosing(e);
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
