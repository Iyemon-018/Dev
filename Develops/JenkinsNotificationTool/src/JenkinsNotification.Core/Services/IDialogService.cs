namespace JenkinsNotification.Core.Services
{
    /// <summary>
    /// ダイアログを表示するためのサービス インターフェースです。
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// 情報ダイアログを表示します。
        /// </summary>
        /// <param name="message">表示メッセージ</param>
        /// <remarks>
        /// [OK]ボタンのみ。情報アイコンを表示するダイアログを表示します。
        /// </remarks>
        void ShowInformation(string message);

        /// <summary>
        /// 質問ダイアログを表示します。
        /// </summary>
        /// <param name="message">表示メッセージ</param>
        /// <returns>true:[Yes]ボタンをクリック, false:[No]ボタンをクリック、もしくは[Esc]</returns>
        /// <remarks>
        /// [Yes], [No]ボタンを表示します。質問アイコンを表示するダイアログを表示します。
        /// </remarks>
        bool ShowQuestion(string message);

        /// <summary>
        /// 警告ダイアログを表示します。
        /// </summary>
        /// <param name="message">表示メッセージ</param>
        /// <returns>true:[Yes]ボタンをクリック, false:[No]ボタンをクリック、もしくは[Esc]</returns>
        /// <remarks>
        /// [Yes], [No]ボタンを表示します。警告アイコンを表示するダイアログを表示します。
        /// </remarks>
        bool ShowWarning(string message);

        /// <summary>
        /// エラーダイアログを表示します。
        /// </summary>
        /// <param name="message">表示メッセージ</param>
        /// <remarks>
        /// [OK]ボタンのみ。エラー アイコンを表示するダイアログを表示します。
        /// </remarks>
        void ShowError(string message);
    }
}