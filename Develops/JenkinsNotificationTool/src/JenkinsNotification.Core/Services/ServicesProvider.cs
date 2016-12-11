namespace JenkinsNotification.Core.Services
{
    using System;

    /// <summary>
    /// 各種サービスを提供するクラスです。
    /// </summary>
    /// <seealso cref="IServicesProvider" />
    public class ServicesProvider : IServicesProvider
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
        /// <param name="dialogService">ダイアログ表示サービス</param>
        /// <param name="viewService">画面表示サービス</param>
        public ServicesProvider(IDialogService dialogService, IViewService viewService)
        {
            if (dialogService == null) throw new ArgumentNullException(nameof(dialogService));
            if (viewService == null) throw new ArgumentNullException(nameof(viewService));

            _dialogService = dialogService;
            _viewService   = viewService;
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