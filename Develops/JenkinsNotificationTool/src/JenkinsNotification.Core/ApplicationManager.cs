namespace JenkinsNotification.Core
{
    using JenkinsNotification.Core.Configurations;

    public sealed class ApplicationManager
    {
        public static ApplicationManager Instance => new ApplicationManager();

        internal ApplicationManager()
        {
            
        }

        public ApplicationConfiguration ApplicationConfiguration => ApplicationConfiguration.Current;
    }
}