namespace JenkinsNotificationTool.Views
{
    using System.ComponentModel;
    using System.Windows;
    using JenkinsNotification.Core.ComponentModels;
    using JenkinsNotification.Core.Services;
    using JenkinsNotification.CustomControls;
    using JenkinsNotification.CustomControls.Services;
    using JenkinsNotificationTool.ViewModels;

    /// <summary>
    /// MainView.xaml の相互作用ロジック
    /// </summary>
    [ViewModel(typeof(MainViewModel))]
    public partial class MainView : View
    {
        #region Ctor

        public MainView()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        protected override void OnClosing(CancelEventArgs e)
        {
            TaskbarIcon.Dispose();
            base.OnClosing(e);
        }

        private void MainView_OnLoaded(object sender, RoutedEventArgs e)
        {
            
        }

        #endregion
    }
}
