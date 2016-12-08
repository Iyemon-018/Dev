namespace JenkinsNotification.Core.Services
{
    /// <summary>
    /// 各種サービスのインジェクション サービス インターフェースです。
    /// </summary>
    public interface IInjectionService
    {
        /// <summary>
        /// ダイアログ表示サービスを取得します。
        /// </summary>
        IDialogService DialogService { get; }

        /// <summary>
        /// 画面表示サービスを取得します。
        /// </summary>
        IViewService ViewService { get; }
    }
}