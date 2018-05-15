namespace JenkinsNotification.Core.Configurations.Verify
{
    using System;

    /// <summary>
    /// 構成情報の検証結果が異常だった場合にスロされる例外クラスです。
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class ConfigurationVerifyException : Exception
    {
        #region Ctor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="filePath">検証を行った構成ファイルのパス</param>
        /// <param name="result">検証結果</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="result"/> がnull の場合にスローされます。</exception>
        public ConfigurationVerifyException(string message, string filePath, VerifyResult result)
            : base(message + $"Path {filePath}" + Environment.NewLine + result)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));
            Result   = result;
            FilePath = filePath;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 検証結果を取得します。
        /// </summary>
        public VerifyResult Result { get; private set; }

        /// <summary>
        /// 検証を行った構成ファイルのパスを取得します。
        /// </summary>
        public string FilePath { get; private set; }

        #endregion
    }
}