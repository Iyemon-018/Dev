namespace JenkinsNotification.Core.Logs
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using Extensions;

    /// <summary>
    /// ログ出力管理クラスです。
    /// </summary>
    public sealed class LogManager
    {
        #region Const

        /// <summary>
        /// 唯一のインスタンス
        /// </summary>
        private static readonly LogManager _instance = new LogManager();

        #endregion

        #region Ctor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private LogManager()
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// 唯一のインスタンスの参照を取得します。
        /// </summary>
        internal static LogManager Instance => _instance;

        #endregion

        #region Methods

        /// <summary>
        /// デバッグメッセージを出力します。
        /// </summary>
        /// <param name="message">出力するメッセージ</param>
        /// <param name="filePath">出力元のファイルパス(設定不要)</param>
        /// <param name="memberName">出力元のメンバー名(設定不要)</param>
        /// <param name="lineNumber">出力元のファイル行数(設定不要)</param>
        public static void Debug(string message,
                                 [CallerFilePath] string filePath = null,
                                 [CallerMemberName] string memberName = null,
                                 [CallerLineNumber] int lineNumber = 0)
        {
            Instance.Output(LogLevel.Debug, message, filePath, memberName, lineNumber);
        }

        /// <summary>
        /// エラーメッセージを出力します。
        /// </summary>
        /// <param name="message">出力するメッセージ</param>
        /// <param name="filePath">出力元のファイルパス(設定不要)</param>
        /// <param name="memberName">出力元のメンバー名(設定不要)</param>
        /// <param name="lineNumber">出力元のファイル行数(設定不要)</param>
        public static void Error(string message,
                                 [CallerFilePath] string filePath = null,
                                 [CallerMemberName] string memberName = null,
                                 [CallerLineNumber] int lineNumber = 0)
        {
            Instance.Output(LogLevel.Error, message, filePath, memberName, lineNumber);
        }

        /// <summary>
        /// エラーメッセージを出力します。
        /// </summary>
        /// <param name="message">出力するメッセージ</param>
        /// <param name="exception">補足した例外オブジェクト</param>
        /// <param name="filePath">出力元のファイルパス(設定不要)</param>
        /// <param name="memberName">出力元のメンバー名(設定不要)</param>
        /// <param name="lineNumber">出力元のファイル行数(設定不要)</param>
        public static void Error(string message,
                                 Exception exception,
                                 [CallerFilePath] string filePath = null,
                                 [CallerMemberName] string memberName = null,
                                 [CallerLineNumber] int lineNumber = 0)
        {
            Instance.Output(LogLevel.Error, message, filePath, memberName, lineNumber);
            foreach (var traceMessage in exception.ToStackTraceMessages())
            {
                Instance.Output(LogLevel.Error, traceMessage, filePath, memberName, lineNumber);
            }
        }

        /// <summary>
        /// 深刻なエラーメッセージを出力します。
        /// </summary>
        /// <param name="message">出力するメッセージ</param>
        /// <param name="filePath">出力元のファイルパス(設定不要)</param>
        /// <param name="memberName">出力元のメンバー名(設定不要)</param>
        /// <param name="lineNumber">出力元のファイル行数(設定不要)</param>
        public static void Fatal(string message,
                                 [CallerFilePath] string filePath = null,
                                 [CallerMemberName] string memberName = null,
                                 [CallerLineNumber] int lineNumber = 0)
        {
            Instance.Output(LogLevel.Fatal, message, filePath, memberName, lineNumber);
        }

        /// <summary>
        /// 深刻なエラーメッセージを出力します。
        /// </summary>
        /// <param name="message">出力するメッセージ</param>
        /// <param name="exception">補足した例外オブジェクト</param>
        /// <param name="filePath">出力元のファイルパス(設定不要)</param>
        /// <param name="memberName">出力元のメンバー名(設定不要)</param>
        /// <param name="lineNumber">出力元のファイル行数(設定不要)</param>
        public static void Fatal(string message,
                                 Exception exception,
                                 [CallerFilePath] string filePath = null,
                                 [CallerMemberName] string memberName = null,
                                 [CallerLineNumber] int lineNumber = 0)
        {
            Instance.Output(LogLevel.Fatal, message, filePath, memberName, lineNumber);
            foreach (var traceMessage in exception.ToStackTraceMessages())
            {
                Instance.Output(LogLevel.Fatal, traceMessage, filePath, memberName, lineNumber);
            }
        }

        /// <summary>
        /// 情報メッセージを出力します。
        /// </summary>
        /// <param name="message">出力するメッセージ</param>
        /// <param name="filePath">出力元のファイルパス(設定不要)</param>
        /// <param name="memberName">出力元のメンバー名(設定不要)</param>
        /// <param name="lineNumber">出力元のファイル行数(設定不要)</param>
        public static void Info(string message,
                                [CallerFilePath] string filePath = null,
                                [CallerMemberName] string memberName = null,
                                [CallerLineNumber] int lineNumber = 0)
        {
            Instance.Output(LogLevel.Information, message, filePath, memberName, lineNumber);
        }

        /// <summary>
        /// トレースメッセージを出力します。
        /// </summary>
        /// <param name="message">出力するメッセージ</param>
        /// <param name="filePath">出力元のファイルパス(設定不要)</param>
        /// <param name="memberName">出力元のメンバー名(設定不要)</param>
        /// <param name="lineNumber">出力元のファイル行数(設定不要)</param>
        public static void Trace(string message,
                                 [CallerFilePath] string filePath = null,
                                 [CallerMemberName] string memberName = null,
                                 [CallerLineNumber] int lineNumber = 0)
        {
            Instance.Output(LogLevel.Trace, message, filePath, memberName, lineNumber);
        }

        /// <summary>
        /// 警告メッセージを出力します。
        /// </summary>
        /// <param name="message">出力するメッセージ</param>
        /// <param name="filePath">出力元のファイルパス(設定不要)</param>
        /// <param name="memberName">出力元のメンバー名(設定不要)</param>
        /// <param name="lineNumber">出力元のファイル行数(設定不要)</param>
        public static void Warning(string message,
                                [CallerFilePath] string filePath = null,
                                [CallerMemberName] string memberName = null,
                                [CallerLineNumber] int lineNumber = 0)
        {
            Instance.Output(LogLevel.Warning, message, filePath, memberName, lineNumber);
        }

        /// <summary>
        /// ログを出力します。
        /// </summary>
        /// <param name="level">出力レベル</param>
        /// <param name="message">出力するメッセージ</param>
        /// <param name="filePath">出力元のファイルパス(設定不要)</param>
        /// <param name="memberName">出力元のメンバー名(設定不要)</param>
        /// <param name="lineNumber">出力元のファイル行数(設定不要)</param>
        private void Output(LogLevel level, string message, string filePath, string memberName, int lineNumber)
        {
            var fileName = Path.GetFileName(filePath);
            Console.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss}|[{level}]|{fileName}/{memberName}/Line:{lineNumber}|{message}");
        }

        #endregion
    }
}