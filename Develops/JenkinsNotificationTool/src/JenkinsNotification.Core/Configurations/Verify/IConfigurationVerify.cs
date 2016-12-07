namespace JenkinsNotification.Core.Configurations.Verify
{
    public interface IConfigurationVerify<T> where T:class 
    {
        VerifyResult Verify(T config);
    }
}