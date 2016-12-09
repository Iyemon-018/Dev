namespace JenkinsNotification.Core.Utility
{
    using Configurations.Verify;
    using Extensions;
    using JenkinsNotification.Core.Properties;

    /// <summary>
    /// 構成ファイルに関するユーティリティ機能クラスです。
    /// </summary>
    internal static class ConfigurationUtility
    {
        #region Methods

        /// <summary>
        /// 指定したファイルから構成情報を読み込み、検証します。
        /// </summary>
        /// <typeparam name="T">読み込み対象の構成情報の型</typeparam>
        /// <param name="filePath">ファイルパス</param>
        /// <param name="verify">構成情報の検証オブジェクト</param>
        /// <returns>構成情報ファイルから読み込んだ構成情報オブジェクト</returns>
        /// <exception cref="ConfigurationVerifyException">構成ファイルの検証結果が異常だった場合にスローされます。</exception>
        public static T Load<T>(string filePath, IConfigurationVerify<T> verify) where T : class, new()
        {
            var config = filePath.Deserialize<T>();
            var result = verify.Verify(config);
            if (!result.Correct)
            {
                throw new ConfigurationVerifyException(Resources.ConfigurationVerifyLoadedErrorMessage, filePath, result);
            }

            return config;
        }

        /// <summary>
        /// 指定した構成情報を検証し、ファイルに保存します。
        /// </summary>
        /// <typeparam name="T">保存対象の構成情報の型</typeparam>
        /// <param name="config">保存対象の構成情報オブジェクト</param>
        /// <param name="filePath">ファイルパス</param>
        /// <param name="verify">構成情報の検証オブジェクト</param>
        /// <exception cref="ConfigurationVerifyException">構成ファイルの検証結果が異常だった場合にスローされます。</exception>
        public static void Save<T>(T config, string filePath, IConfigurationVerify<T> verify) where T : class
        {
            var result = verify.Verify(config);
            if (!result.Correct)
            {
                throw new ConfigurationVerifyException(Resources.ConfigurationVerifySaveErrorMessage, filePath, result);
            }
            config.Serialize(filePath);
        }

        #endregion
    }
}