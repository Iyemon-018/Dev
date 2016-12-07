namespace JenkinsNotification.Core
{
    using System.Windows;
    using Configurations;

    /// <summary>
    /// このアプリケーションの機能管理クラスです。
    /// </summary>
    public sealed class ApplicationManager
    {
        #region Ctor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal ApplicationManager()
        {
            
        }

        #endregion

        #region Properties

        /// <summary>
        /// 唯一のインスタンスを取得します。
        /// </summary>
        public static ApplicationManager Instance => new ApplicationManager();

        /// <summary>
        /// アプリケーション構成情報を取得します。
        /// </summary>
        public ApplicationConfiguration ApplicationConfiguration => ApplicationConfiguration.Current;

        #endregion

        public void Initialize()
        {
            
        }

        public void Shutdown()
        {
            Application.Current.Shutdown();
        }
    }
}