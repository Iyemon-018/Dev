namespace NotifyIconSample
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;

    public class MainWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// 終了実行コマンドを設定、取得します。
        /// </summary>
        public RelayCommand ExitCommand { get; private set; }

        /// <summary>
        /// 標準バルーン表示実行コマンドを設定、取得します。
        /// </summary>
        public RelayCommand ShowBalloonCommand { get; private set; }

        /// <summary>
        /// カスタム バルーン実行コマンドを設定、取得します。
        /// </summary>
        public RelayCommand ShowCustomBalloonCommand { get; private set; }

        /// <summary>
        /// ２重にバルーン表示実行コマンドを設定、取得します。
        /// </summary>
        public RelayCommand ShowTwiceBalloonCommand { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks>
        /// デザイナー用
        /// </remarks>
        public MainWindowViewModel() : this(null)
        {
            
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="balloonTipService">The balloon tip service.</param>
        public MainWindowViewModel(IBalloonTipService balloonTipService) : base(balloonTipService)
        {
            ExitCommand = new RelayCommand(x => Application.Current.Shutdown());
            ShowBalloonCommand = new RelayCommand(x => BalloonTipService.NotifyInformation("テスト", "ViewModelからバルーンチップを表示しています。"));
            ShowCustomBalloonCommand
                = new RelayCommand(x => BalloonTipService.NotifyCustom("カスタム バルーン テスト", "ViewModelからバルーンチップを表示しています。", null));
            ShowTwiceBalloonCommand = new RelayCommand(async x =>
                                                       {
                                                           // ２回に分けてバルーンを表示する。
                                                           // この場合、２回めのバルーンを表示するときに１回めのバルーンは消える。
                                                           BalloonTipService.NotifyCustom("複数バルーン テスト1", "ViewModelからバルーンチップを表示しています。", null);
                                                           await Task.Delay(TimeSpan.FromSeconds(3));
                                                           BalloonTipService.NotifyCustom("複数バルーン テスト2", "ViewModelからバルーンチップを表示しています。", null);
                                                       });
        }
    }
}