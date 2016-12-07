namespace JenkinsNotification.CustomControls.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Hardcodet.Wpf.TaskbarNotification;
    using JenkinsNotification.Core;
    using JenkinsNotification.Core.Configurations;
    using JenkinsNotification.Core.Services;
    using JenkinsNotification.Core.ViewModels.Api;
    using JenkinsNotification.CustomControls.BalloonTips;

    /// <summary>
    /// バルーン通知サービス クラスです。
    /// </summary>
    /// <seealso cref="JenkinsNotification.Core.Services.IBalloonTipService" />
    public class BalloonTipService : IBalloonTipService
    {
        #region Fields

        /// <summary>
        /// アプリケーション構成情報の参照
        /// </summary>
        private readonly ApplicationConfiguration _config = ApplicationManager.Instance.ApplicationConfiguration;

        /// <summary>
        /// バルーン通知を表示するための<see cref="TaskbarIcon"/>
        /// </summary>
        private readonly TaskbarIcon _taskbarIcon;

        #endregion

        #region Ctor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="taskbarIcon">タスクバー アイコン オブジェクト</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="taskbarIcon"/> がnull の場合にスローされます。</exception>
        public BalloonTipService(TaskbarIcon taskbarIcon)
        {
            if (taskbarIcon == null) throw new ArgumentNullException(nameof(taskbarIcon));
            _taskbarIcon = taskbarIcon;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 異常通知バルーンを表示します。
        /// </summary>
        /// <param name="title">タイトル</param>
        /// <param name="message">メッセージ</param>
        public void NotifyError(string title, string message)
        {
            Notify(title, message, BalloonIcon.Error);
        }

        /// <summary>
        /// 情報通知バルーンを表示します。
        /// </summary>
        /// <param name="title">タイトル</param>
        /// <param name="message">メッセージ</param>
        public void NotifyInformation(string title, string message)
        {
            Notify(title, message, BalloonIcon.Info);
        }

        /// <summary>
        /// ジョブ結果通知バルーンを表示します。
        /// </summary>
        /// <param name="executeResult">ジョブ実行結果</param>
        public void NotifyJobResult(IJobExecuteResult executeResult)
        {
            var balloon = new JobExecuteResultBalloonTip(executeResult);
            _taskbarIcon.ShowCustomBalloon(balloon,
                                           _config.NotifyConfiguration.PopupAnimationType,
                                           (int?)_config.NotifyConfiguration.PopupTimeout?.TotalMilliseconds);
        }

        /// <summary>
        /// 警告通知バルーンを表示します。
        /// </summary>
        /// <param name="title">タイトル</param>
        /// <param name="message">メッセージ</param>
        public void NotifyWarning(string title, string message)
        {
            Notify(title, message, BalloonIcon.Warning);
        }

        /// <summary>
        /// 標準の通知バルーンを表示します。
        /// </summary>
        /// <param name="title">タイトル</param>
        /// <param name="message">メッセージ</param>
        /// <param name="symbol">表示アイコン</param>
        private void Notify(string title, string message, BalloonIcon symbol)
        {
            _taskbarIcon.ShowBalloonTip(title, message, symbol);
        }

        #endregion
    }
}