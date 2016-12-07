namespace JenkinsNotification.Core.Configurations.Verify
{
    using System;
    using System.Runtime.Serialization;

    public class ConfigurationVerifyException : Exception
    {
        public VerifyResult Result { get; private set; }

        public string FilePath { get; private set; }

        public ConfigurationVerifyException(string message, string filePath, VerifyResult result)
            : base(message + $"Path {filePath}" + Environment.NewLine + result)
        {
            Result = result;
            FilePath = filePath;
        }
    }
}