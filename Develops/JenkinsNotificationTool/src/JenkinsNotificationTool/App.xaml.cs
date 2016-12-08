namespace JenkinsNotificationTool
{
    using System.Windows;
    using JenkinsNotification.Core;
    using JenkinsNotification.Core.Services;
    using JenkinsNotification.CustomControls.Services;
    using JenkinsNotificationTool.Views;

    /// <summary>
    /// このアプリケーションのエントリポイントです。
    /// </summary>
    public partial class App : Application
    {
        #region Methods

        /// <summary>
        /// <see cref="E:System.Windows.Application.Startup" /> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント データを格納している <see cref="T:System.Windows.StartupEventArgs" />。</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            //
            // アプリケーションで使いまわすインジェクション サービスを設定する。
            //
            ApplicationManager.SetDefaultViewModelLocater(new InjectionService());

            base.OnStartup(e);

            //
            // メインウィンドウの初期化を行う。
            // タスクトレイに表示するだけでいいのでShow()は必要ない。
            // ShutdownMode="OnExplicitShutdown"
            // に設定しているのでShutdown() が唯一のアプリケーション終了方法となっている。
            //
            var view = new MainView();

            //
            // アプリケーション機能の初期化を実施する。
            //
            ApplicationManager.Initialize(new BalloonTipService(view.TaskbarIcon));
        }

        #endregion
    }
}
