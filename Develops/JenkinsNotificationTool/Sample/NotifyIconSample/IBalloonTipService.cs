namespace NotifyIconSample
{
    using System;

    public interface IBalloonTipService
    {
        void Notify(string title, string message);

        void NotifyInformation(string title, string message);

        void NotifyWarning(string title, string message);

        void NotifyError(string title, string message);

        void NotifyCustom(string title, string message, TimeSpan? timeout);
    }
}