namespace JenkinsNotification.Core.Configurations.Verify
{
    public class ApplicationConfigurationVerify : IConfigurationVerify<ApplicationConfiguration>
    {
        public VerifyResult Verify(ApplicationConfiguration config)
        {
            var result = new VerifyResult();

            var notifyConfigVerify = new NotifyConfigurationVerify();
            result = notifyConfigVerify.Verify(config.NotifyConfiguration);
            if (!result.Correct)
            {
                return result;
            }

            // TODO 他の設定ファイルの検証も実装する。

            return result;
        }
    }
}