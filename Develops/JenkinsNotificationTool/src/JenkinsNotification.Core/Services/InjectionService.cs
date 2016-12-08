namespace JenkinsNotification.Core.Services
{
    /// <summary>
    /// 各種サービスのインジェクション サービス クラスです。
    /// </summary>
    /// <seealso cref="IInjectionService" />
    public class InjectionService : IInjectionService
    {
        #region Fields

        /// <summary>
        /// ダイアログ表示サービス
        /// </summary>
        private readonly IDialogService _dialogService;

        /// <summary>
        /// 画面表示サービス
        /// </summary>
        private readonly IViewService _viewService;

        #endregion

        #region Ctor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public InjectionService()
        {
            _dialogService = new DialogService();
            _viewService   = new ViewService();
        }

        #endregion

        #region Properties

        /// <summary>
        /// ダイアログ表示サービスを取得します。
        /// </summary>
        public IDialogService DialogService => _dialogService;

        /// <summary>
        /// 画面表示サービスを取得します。
        /// </summary>
        public IViewService ViewService => _viewService;

        #endregion
    }
}