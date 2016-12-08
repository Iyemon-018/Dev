namespace JenkinsNotification.Core.ComponentModels
{
    using System;
    using JenkinsNotification.Core.Services;
    using JenkinsNotification.Core.Utility;

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
        private readonly IInjectionService _injectionService;

        #region Ctor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected ApplicationViewModelBase(IInjectionService injectionService)
        {
            if (ViewUtility.IsDesignMode()) return;
            _injectionService = injectionService;
            DialogService = injectionService.DialogService;
            ViewService = injectionService.ViewService;
        }

        #endregion

        #region Properties

        /// <summary>
        /// <see cref="ApplicationManager"/> の参照を取得します。
        /// </summary>
        protected ApplicationManager ApplicationManager => ApplicationManager.Instance;

        protected IBalloonTipService BalloonTipService => ApplicationManager.BalloonTipService;
        
        protected IViewService ViewService { get; private set; }

        /// <summary>
        /// ダイアログ サービスを取得します。
        /// </summary>
        protected IDialogService DialogService { get; private set; }

        #endregion
    }
}