namespace JenkinsNotification.Core.Services
{
    using JenkinsNotification.Core.ViewModels.Api;

    /// <summary>
    /// バルーン通知を表示するためのサービス インターフェースです。
    /// </summary>
    public interface IBalloonTipService
    {
        /// <summary>
        /// 情報通知バルーンを表示します。
        /// </summary>
        /// <param name="title">タイトル</param>
        /// <param name="message">メッセージ</param>
        void NotifyInformation(string title, string message);

        /// <summary>
        /// 警告通知バルーンを表示します。
        /// </summary>
        /// <param name="title">タイトル</param>
        /// <param name="message">メッセージ</param>
        void NotifyWarning(string title, string message);

        /// <summary>
        /// 異常通知バルーンを表示します。
        /// </summary>
        /// <param name="title">タイトル</param>
        /// <param name="message">メッセージ</param>
        void NotifyError(string title, string message);

        /// <summary>
        /// ジョブ結果通知バルーンを表示します。
        /// </summary>
        /// <param name="executeResult">ジョブ実行結果</param>
        void NotifyJobResult(IJobExecuteResult executeResult);
    }
}