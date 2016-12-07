namespace JenkinsNotification.Core.Configurations
{
    using System;
    using System.IO;
    using JenkinsNotification.Core.Configurations.Verify;
    using JenkinsNotification.Core.Utility;

    public partial class ApplicationConfiguration : SerializableBase<ApplicationConfiguration>
    {
        private static readonly ApplicationConfiguration _current;

        public static ApplicationConfiguration Current => _current;

        public override string DefaultFilePath => Path.Combine(PathUtility.AppTempPath, "ApplicationConfiguration.xml");

        protected override IConfigurationVerify<ApplicationConfiguration> Verify => new ApplicationConfigurationVerify();

        public static void LoadCurrent()
        {
            _current.Load();
        }

        public static void SaveCurrent()
        {
            _current.Save();
        }
    }
}