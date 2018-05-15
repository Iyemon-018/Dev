namespace JenkinsNotification.CustomControls.Services
{
    using System.Windows;
    using Core.Services;
    using Core.Utility;

    /// <summary>
    /// ダイアログ表示のサービス クラスです。
    /// </summary>
    /// <seealso cref="IDialogService" />
    public class DialogService : IDialogService
    {
        #region Methods

        /// <summary>
        /// エラーダイアログを表示します。
        /// </summary>
        /// <param name="message">表示メッセージ</param>
        /// <remarks>[OK]ボタンのみ。エラー アイコンを表示するダイアログを表示します。</remarks>
        public void ShowError(string message)
        {
            Show(message, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// 情報ダイアログを表示します。
        /// </summary>
        /// <param name="message">表示メッセージ</param>
        /// <remarks>[OK]ボタンのみ。情報アイコンを表示するダイアログを表示します。</remarks>
        public void ShowInformation(string message)
        {
            Show(message, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// 質問ダイアログを表示します。
        /// </summary>
        /// <param name="message">表示メッセージ</param>
        /// <returns>true:[Yes]ボタンをクリック, false:[No]ボタンをクリック、もしくは[Esc]</returns>
        /// <remarks>[Yes], [No]ボタンを表示します。質問アイコンを表示するダイアログを表示します。</remarks>
        public bool ShowQuestion(string message)
        {
            return Show(message, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
        }

        /// <summary>
        /// 警告ダイアログを表示します。
        /// </summary>
        /// <param name="message">表示メッセージ</param>
        /// <returns>true:[Yes]ボタンをクリック, false:[No]ボタンをクリック、もしくは[Esc]</returns>
        /// <remarks>[Yes], [No]ボタンを表示します。警告アイコンを表示するダイアログを表示します。</remarks>
        public bool ShowWarning(string message)
        {
            return Show(message, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes;
        }

        /// <summary>
        /// メッセージボックスを表示します。
        /// </summary>
        /// <param name="message">メッセージテキスト</param>
        /// <param name="button">表示ボタン種別</param>
        /// <param name="icon">表示アイコン種別</param>
        /// <returns>メッセージボックス選択結果</returns>
        private MessageBoxResult Show(string message, MessageBoxButton button, MessageBoxImage icon)
        {
            var title = Products.Current.Title;
            var owner = ViewUtility.GetActiveWindow();
            return owner == null
                ? MessageDialog.Show(message, title, button, icon)
                : MessageDialog.Show(owner, message, title, button, icon);
        }

        #endregion
    }
}