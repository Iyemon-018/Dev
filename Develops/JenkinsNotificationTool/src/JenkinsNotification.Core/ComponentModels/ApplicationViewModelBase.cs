namespace JenkinsNotification.Core.ComponentModels
{
    using System;
    using JenkinsNotification.Core.Services;

    /// <summary>
    /// Viewと対になる、このアプリケーション専用のViewModel クラスです。
    /// </summary>
    /// <seealso cref="JenkinsNotification.Core.ComponentModels.ViewModelBase" />
    /// <remarks>
    /// このViewModelは、以下の機能を備えています。<para/>
    /// ・メッセージ ダイアログを表示する。
    /// </remarks>
    public abstract class ApplicationViewModelBase : ViewModelBase
    {
        #region Ctor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dialogService">ダイアログ サービス</param>
        protected ApplicationViewModelBase(IDialogService dialogService)
        {
            if (dialogService == null) throw new ArgumentNullException(nameof(dialogService));
            DialogService = dialogService;
        }

        #endregion

        #region Properties

        /// <summary>
        /// <see cref="ApplicationManager"/> の参照を取得します。
        /// </summary>
        protected ApplicationManager ApplicationManager => ApplicationManager.Instance;

        /// <summary>
        /// ダイアログ サービスを取得します。
        /// </summary>
        protected IDialogService DialogService { get; private set; }

        #endregion
    }
}