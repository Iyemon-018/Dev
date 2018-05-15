namespace JenkinsNotification.Core.Configurations
{
    /// <summary>
    /// このアプリケーションの構成ファイル情報クラスです。
    /// </summary>
    /// <remarks>
    /// このファイルには構成ファイルに使用するプロパティのみ定義します。
    /// </remarks>
    public partial class ApplicationConfiguration
    {
        #region Properties

        /// <summary>
        /// 表示関連の構成情報を設定、取得します。
        /// </summary>
        public DisplayConfiguration DisplayConfiguration { get; set; }

        /// <summary>
        /// 通知関連の構成情報を設定、取得します。
        /// </summary>
        public NotifyConfiguration NotifyConfiguration { get; set; }

        #endregion
    }
}