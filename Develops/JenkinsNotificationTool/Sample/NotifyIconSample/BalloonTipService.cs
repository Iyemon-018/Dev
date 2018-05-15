namespace NotifyIconSample
{
    using System;
    using System.Windows.Controls.Primitives;
    using Hardcodet.Wpf.TaskbarNotification;
    using NotifyIconSample.Annotations;

    public class BalloonTipService : IBalloonTipService
    {
        private readonly TaskbarIcon _taskbarIcon;

        public BalloonTipService([NotNull] TaskbarIcon taskbarIcon)
        {
            if (taskbarIcon == null) throw new ArgumentNullException(nameof(taskbarIcon));
            _taskbarIcon = taskbarIcon;
        }

        private void Show(string title, string message, BalloonIcon icon)
        {
            _taskbarIcon.ShowBalloonTip(title, message, icon);
        }
        
        public void Notify(string title, string message)
        {
            Show(title, message, BalloonIcon.None);
        }

        public void NotifyInformation(string title, string message)
        {
            Show(title, message, BalloonIcon.Info);
        }

        public void NotifyWarning(string title, string message)
        {
            Show(title, message, BalloonIcon.Warning);
        }

        public void NotifyError(string title, string message)
        {
            Show(title, message, BalloonIcon.Error);
        }

        public void NotifyCustom(string title, string message, TimeSpan? timeout)
        {
            var balloon = new CustomBalloon(_taskbarIcon)
                          {
                              Title = title,
                              Message = message,
                          };
            
            //
            // 複数の通知を表示したりとかはできないみたい。
            //
            _taskbarIcon.ShowCustomBalloon(balloon, PopupAnimation.Fade, (int?)timeout?.TotalMilliseconds);
        }
    }
}