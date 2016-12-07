namespace JenkinsNotification.Core.Configurations
{
    using System;
    using System.Configuration;
    using JenkinsNotification.Core.Configurations.Verify;
    using JenkinsNotification.Core.Extensions;

    public abstract class SerializableBase<T>
            where T : class, new()
    {
        public abstract string DefaultFilePath { get; }

        protected abstract IConfigurationVerify<T> Verify { get; }

        public T Load(string filePath = null)
        {
            if (filePath == null)
            {
                filePath = DefaultFilePath;
            }

            return OnLoad(filePath);
        }

        protected virtual T OnLoad(string filePath)
        {
            var config = filePath.Deserialize<T>();
            var result = Verify.Verify(config);
            if (!result.Correct)
            {
                throw new ConfigurationVerifyException("読み込み後の構成ファイル検証が異常でした。", filePath, result);
            }

            return config;
        }

        public void Save(string filePath = null)
        {
            if (filePath == null)
            {
                filePath = DefaultFilePath;
            }

            var config = this as T;
            OnSave(config, filePath);
        }

        private void OnSave(T config, string filePath)
        {
            var result = Verify.Verify(config);
            if (!result.Correct)
            {
                throw new ConfigurationVerifyException("保存時の構成ファイル検証が異常でした。", filePath, result);
            }
            this.Serialize(filePath);
        }
    }
}