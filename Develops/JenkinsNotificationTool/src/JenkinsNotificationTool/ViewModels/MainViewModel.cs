namespace JenkinsNotificationTool.ViewModels
{
    using System.Windows;
    using JenkinsNotification.Core.ComponentModels;
    using JenkinsNotification.Core.Services;
    using JenkinsNotification.CustomControls.Services;
    using Microsoft.Practices.Prism.Commands;

    public class MainViewModel : ApplicationViewModelBase
    {
        public MainViewModel()
            : this(null)
        {

        }

        public MainViewModel(IInjectionService injectionService) : base(injectionService)
        {
            //
            // 各コマンドの初期化を行う。
            //

            //
            // 終了コマンド
            // コンテキストメニューからメッセージボックスを表示すると即時クローズされてしまうので
            // メッセージ無しで終了する。
            //
            ExitCommand = new DelegateCommand(() => ApplicationManager.Shutdown());

            ConfigurationCommand = new DelegateCommand(() =>
                                                       {
                                                           // TODO 構成情報画面を表示する。
                                                       });


            ReceivedNotificationListCommand = new DelegateCommand(() =>
                                                                  {
                                                                      // TODO 通知受信履歴一覧を表示する。
                                                                  });

            ShowBalloonCommand = new DelegateCommand(() =>
                                                     {
                                                         BalloonTipService.NotifyInformation("Test", "テスト的にバルーン出した。");
                                                     });
        }

        public DelegateCommand ExitCommand { get; private set; }

        public DelegateCommand ConfigurationCommand { get; private set; }

        public DelegateCommand ReceivedNotificationListCommand { get; private set; }

        public DelegateCommand ShowBalloonCommand { get; private set; }
    }
}