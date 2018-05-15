namespace JenkinsNotification.Core.Configurations
{
    using System;
    using System.Windows.Controls.Primitives;

    /// <summary>
    /// 通知に関する設定情報クラスです。
    /// </summary>
    [Serializable]
    public class NotifyConfiguration
    {
        #region Properties

        /// <summary>
        /// WebSocketの接続先URIを設定、または取得します。
        /// </summary>
        public string TargetUri { get; set; }

        /// <summary>
        /// バルーン通知のアニメーション種別を設定、または取得します。
        /// </summary>
        public PopupAnimation PopupAnimationType { get; set; }

        /// <summary>
        /// バルーン通知が消えるまでのタイムアウトを設定、または取得します。
        /// </summary>
        /// <remarks>
        /// null の場合、タイムアウトはありません。
        /// </remarks>
        public TimeSpan? PopupTimeout { get; set; }

        /// <summary>
        /// 通知の受信履歴に表示する最大データ数を設定、または取得します。
        /// </summary>
        public int DisplayHistoryCount { get; set; }

        /// <summary>
        /// ジョブ結果が成功だった場合でも通知するかどうかを設定、または取得します。
        /// </summary>
        public bool IsNotifySuccess { get; set; }

        #endregion
    }
}