namespace JenkinsNotification.Core.Utility
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using JenkinsNotification.Core.Extensions;

    /// <summary>
    /// 各種パスに関するユーティリティ機能クラスです。
    /// </summary>
    public static class PathUtility
    {
        #region Const

        /// <summary>
        /// アプリケーションのカレントディレクトリ
        /// </summary>
        public static readonly string CurrentPath = Environment.CurrentDirectory;

        /// <summary>
        /// アプリケーションの一時フォルダパス
        /// </summary>
        public static readonly string AppTempPath =
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                             Products.Current.Company,
                             Products.Current.Title);

        /// <summary>
        /// アプリケーションのログフォルダパス
        /// </summary>
        public static readonly string LogPath = Path.Combine(AppTempPath, "Logs");

        /// <summary>
        /// XML のファイルパターンを表します。
        /// </summary>
        public static readonly string XmlFilePattern = "*.xml";

        /// <summary>
        /// アプリケーションのカレント ルート パス
        /// </summary>
        public static readonly string ExecuteRootPath = Path.GetPathRoot(CurrentPath);

        /// <summary>
        /// アプリケーションのカレント ドライブ文字列
        /// </summary>
        public static readonly string ExecuteDriveString = ExecuteRootPath.Substring(0, 1);

        #endregion

        #region Methods

        /// <summary>
        /// 指定したView のURI文字列を生成します。
        /// </summary>
        /// <param name="assemblyName">View の配置されているアセンブリ名</param>
        /// <param name="viewFolderPath">
        /// アセンブリを基準とした、View のフォルダパス<para/>
        /// 例) Example プロジェクトの "ViewModels\Example" フォルダの場合、以下のように指定する。<para/>
        ///     "ViewModels/Example"
        /// </param>
        /// <param name="viewName">拡張子付きのView名称</param>
        /// <returns>URI文字列</returns>
        public static string FactoryFrameSourceUriString(string assemblyName, string viewFolderPath, string viewName)
        {
            return $"/{assemblyName};component/{viewFolderPath}/{viewName}";
        }

        /// <summary>
        /// 指定したディレクトリパスから特定のパターンに一致するファイルパスを全て取得します。
        /// </summary>
        /// <param name="directoryPath">ディレクトリパス</param>
        /// <param name="searchPattern">
        /// 取得対象のファイルパターン文字列<para/>
        /// 空文字の場合は、全てのファイルパスを取得します。
        /// </param>
        /// <returns>ファイルパスリスト</returns>
        public static IEnumerable<string> GetFilePathList(string directoryPath, string searchPattern)
        {
            if (directoryPath.IsEmpty() || !Directory.Exists(directoryPath))
            {
                return Enumerable.Empty<string>();
            }

            //
            // Directory.GetFiles の第二引数はnull だと例外が発生するがstring.Empty だと
            // 空の配列を返すので、string.Empty を設定している。
            //
            var pattern = searchPattern ?? string.Empty;
            return Directory.GetFiles(directoryPath, pattern);
        }

        #endregion
    }
}