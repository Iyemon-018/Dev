namespace JenkinsNotification.Core.Configurations.Verify
{
    public class NotifyConfigurationVerify : IConfigurationVerify<NotifyConfiguration>
    {
        public static readonly int DisplayHistoryMinimum = 25;

        public static readonly int DisplayHistoryMaximum = 100;

        public VerifyResult Verify(NotifyConfiguration config)
        {
            var displayHistoryCount = config.DisplayHistoryCount;
            if (displayHistoryCount < DisplayHistoryMinimum || DisplayHistoryMaximum < config.DisplayHistoryCount)
            {
                return VerifyResult.Error($"DisplayHistoryCount には、{DisplayHistoryMinimum}～{DisplayHistoryMaximum} を設定してください。");
            }
            
            return new VerifyResult();
        }
    }
}