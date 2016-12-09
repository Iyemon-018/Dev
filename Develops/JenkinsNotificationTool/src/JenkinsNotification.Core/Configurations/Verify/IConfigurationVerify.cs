namespace JenkinsNotification.Core.Configurations.Verify
{
    /// <summary>
    /// 構成情報の検証ロジックインターフェースです。
    /// </summary>
    /// <typeparam name="T">検証対象の構成情報クラスの型</typeparam>
    public interface IConfigurationVerify<in T> where T:class 
    {
        /// <summary>
        /// 構成情報の検証を行います。
        /// </summary>
        /// <param name="config">構成情報オブジェクト</param>
        /// <returns>検証結果</returns>
        VerifyResult Verify(T config);
    }
}