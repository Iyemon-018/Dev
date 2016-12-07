namespace JenkinsNotification.Core.Configurations
{
    using System;
    using System.IO;
    using System.Windows.Controls.Primitives;
    using System.Xml.Serialization;
    using Verify;
    using Utility;

    /// <summary>
    /// このアプリケーションの構成ファイル情報クラスです。
    /// </summary>
    [XmlRoot]
    [Serializable]
    public partial class ApplicationConfiguration
    {
        #region Const

        /// <summary>
        /// 現在の構成情報
        /// </summary>
        private static ApplicationConfiguration _current;

        #endregion

        #region Ctor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ApplicationConfiguration()
        {
            //
            // ここにはファイル読み込みできなかったときのために各構成情報のデフォルト値を設定します。
            //
            NotifyConfiguration = new NotifyConfiguration
                                  {
                                      TargetUri           = "ws://mc-tfserver:8081/jenkins",
                                      PopupAnimationType  = PopupAnimation.Slide,
                                      PopupTimeout        = TimeSpan.FromSeconds(10),
                                      DisplayHistoryCount = 100,
                                      IsNotifySuccess     = false
                                  };
            DisplayConfiguration = new DisplayConfiguration();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 現在の構成情報を取得します。
        /// </summary>
        public static ApplicationConfiguration Current => _current;

        /// <summary>
        /// この構成ファイルのデフォルト パスを取得します。
        /// </summary>
        public static string DefaultFilePath => Path.Combine(PathUtility.AppTempPath, "ApplicationConfiguration.xml");

        #endregion

        #region Methods

        /// <summary>
        /// デフォルト パスのファイルを現在の構成情報に読み込みます。
        /// </summary>
        public static void LoadCurrent()
        {
            _current = ConfigurationUtility.Load(DefaultFilePath, new ApplicationConfigurationVerify());
        }

        /// <summary>
        /// 現在の構成情報をデフォルト パスに保存します。
        /// </summary>
        public static void SaveCurrent()
        {
            ConfigurationUtility.Save(_current, DefaultFilePath, new ApplicationConfigurationVerify());
        }

        #endregion
    }
}