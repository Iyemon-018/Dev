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
using System.Windows.Shapes;

namespace JenkinsNotificationTool.Views
{
    using System.ComponentModel;
    using JenkinsNotification.Core.Services;
    using JenkinsNotification.CustomControls.Services;
    using JenkinsNotificationTool.ViewModels;

    /// <summary>
    /// MainView.xaml の相互作用ロジック
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();

            DataContext = new MainViewModel(new DialogService(), new BalloonTipService(TaskbarIcon));
        }

        private void MainView_OnLoaded(object sender, RoutedEventArgs e)
        {
            
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            TaskbarIcon.Dispose();
            base.OnClosing(e);
        }
    }
}
