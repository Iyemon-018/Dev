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
    /// タスクトレイに表示するアイコンを包含するメインのビュー クラスです。
    /// </summary>
    /// <remarks>
    /// この<see cref="View"/> は<see cref="MainViewModel"/> とバインドします。
    /// </remarks>
    [ViewModel(typeof(MainViewModel))]
    public partial class MainView : View
    {
        #region Ctor

        /// <summary>
        /// コンストラクタ
        /// </summary>
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
        
        #endregion
    }
}
