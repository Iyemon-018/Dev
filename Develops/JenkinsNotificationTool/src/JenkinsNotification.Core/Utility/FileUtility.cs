namespace JenkinsNotification.Core.Utility
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// ファイル操作関連のユーティリティ機能クラスです。
    /// </summary>
    public static class FileUtility
    {
        #region Methods

        /// <summary>
        /// 指定したパスのディレクトリを作成します。<para/>
        /// サブディレクトリも作成します。
        /// </summary>
        /// <param name="path">ディレクトリパス</param>
        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// 現在日付から<paramref name="previousDate"/>よりも後日に作成されたファイルを削除します。。
        /// </summary>
        /// <param name="directory">検索対象のディレクトリパス</param>
        /// <param name="previousDate">
        /// 取得期間の<see cref="T:TimeSpan"/><para/>
        /// 現在日時からこの<see cref="T:TimeSpan"/> 以降の日付に作成されたファイルパスを取得します。
        /// </param>
        /// <returns>削除したファイルの数</returns>
        public static int DeleteFilesForPreviousSpan(string directory, TimeSpan previousDate)
        {
            var result = 0;
            var files = GetFilesForPreviousSpan(directory, previousDate);
            if (!files.Any())
            {
                return result;
            }

            foreach (var file in files)
            {
                RemoveFile(file);
                result++;
            }
            return result;
        }

        /// <summary>
        /// 現在日付から<paramref name="previousDate"/> よりも後日に作成されたファイル パスを取得します。
        /// </summary>
        /// <param name="directory">検索対象のディレクトリパス</param>
        /// <param name="previousDate">
        /// 取得期間の<see cref="T:TimeSpan"/><para/>
        /// 現在日時からこの<see cref="T:TimeSpan"/> 以降の日付に作成されたファイルパスを取得します。
        /// </param>
        /// <returns>
        /// 該当ファイルパス コレクション<para/>
        /// <param name="directory"></param> が存在しない場合は、<see cref="Enumerable.Empty{TResult}"/> を返します。
        /// </returns>
        public static IEnumerable<string> GetFilesForPreviousSpan(string directory, TimeSpan previousDate)
        {
            if (!Directory.Exists(directory))
            {
                // 該当フォルダなし。
                return Enumerable.Empty<string>();
            }

            //
            // 一度、Key=ファイルパス, Value=作成日時 の連想配列に変換し、
            // 日時でフィルターし、その結果をファイルパス配列に変換して返す。
            //
            return Directory.GetFiles(directory)
                            .ToDictionary(x => x, File.GetCreationTime)
                            .Where(x => DateTime.Compare(DateTime.Today.Add(previousDate), x.Value.Date) == 1)
                            .Select(x => x.Key);
        }

        /// <summary>
        /// 指定したファイルを削除します。<para/>
        /// ファイルが存在しない場合は削除が実行されません。
        /// </summary>
        /// <param name="filePath">削除対象のファイルパス</param>
        public static void RemoveFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        
        #endregion
    }
}