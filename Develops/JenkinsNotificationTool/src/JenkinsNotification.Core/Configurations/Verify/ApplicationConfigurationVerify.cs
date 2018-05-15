namespace JenkinsNotification.Core.Configurations.Verify
{
    using System;

    /// <summary>
    /// 構成情報<see cref="ApplicationConfiguration"/> の検証ロジッククラスです。
    /// </summary>
    /// <seealso cref="Configurations.Verify.IConfigurationVerify{ApplicationConfiguration}" />
    public class ApplicationConfigurationVerify : IConfigurationVerify<ApplicationConfiguration>
    {
        #region Methods

        /// <summary>
        /// 構成情報の検証を行います。
        /// </summary>
        /// <param name="config">構成情報オブジェクト</param>
        /// <returns>検証結果</returns>
        public VerifyResult Verify(ApplicationConfiguration config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));

            //
            // 通知関連の構成情報を検証する。
            //
            var notifyConfigVerify = new NotifyConfigurationVerify();
            var result = notifyConfigVerify.Verify(config.NotifyConfiguration);
            if (!result.Correct)
            {
                // 検証エラー
                return result;
            }

            // TODO 他の設定ファイルの検証も実装する。

            return result;
        }

        #endregion
    }
}