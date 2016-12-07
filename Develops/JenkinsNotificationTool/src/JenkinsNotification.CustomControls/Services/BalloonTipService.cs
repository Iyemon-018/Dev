namespace JenkinsNotification.CustomControls.Services
{
    using System;
    using System.Windows.Controls.Primitives;
    using Hardcodet.Wpf.TaskbarNotification;
    using Core;
    using Core.Configurations;
    using Core.Services;
    using Core.ViewModels.Api;
    using BalloonTips;

    public class BalloonTipService : IBalloonTipService
    {
        private readonly TaskbarIcon _taskbarIcon;

        private readonly ApplicationConfiguration _config = ApplicationManager.Instance.ApplicationConfiguration;

        public BalloonTipService(TaskbarIcon taskbarIcon)
        {
            if (taskbarIcon == null) throw new ArgumentNullException(nameof(taskbarIcon));
            _taskbarIcon = taskbarIcon;
        }

        private void Notify(string title, string message, BalloonIcon symbol)
        {
            _taskbarIcon.ShowBalloonTip(title, message, symbol);
        }

        public void NotifyInformation(string title, string message)
        {
            Notify(title, message, BalloonIcon.Info);
        }

        public void NotifyWarning(string title, string message)
        {
            Notify(title, message, BalloonIcon.Warning);
        }

        public void NotifyError(string title, string message)
        {
            Notify(title, message, BalloonIcon.Error);
        }

        public void NotifyJobResult(IJobExecuteResult executeResult)
        {
            var balloon = new JobExecuteResultBalloonTip(executeResult);
            _taskbarIcon.ShowCustomBalloon(balloon,
                                           _config.NotifyConfiguration.PopupAnimationType,
                                           (int?)_config.NotifyConfiguration.PopupTimeout?.TotalMilliseconds);
        }
    }
}