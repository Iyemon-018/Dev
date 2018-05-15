namespace JenkinsNotification.Core
{
    using JenkinsNotification.Core.Jenkins.Api;

    /// <summary>
    /// ジョブの実行結果種別を定義します。
    /// </summary>
    /// <remarks>
    /// <see cref="JobExecuteResult"/> によって取得できる、ジョブのビルド結果です。
    /// </remarks>
    public enum JobResultType
    {
        /// <summary>
        /// 実行結果が不明な値であることを表します。
        /// </summary>
        None = 0,

        /// <summary>
        /// 正常であることを表します。
        /// </summary>
        Success,

        /// <summary>
        /// 警告であることを表します。
        /// </summary>
        Warning,

        /// <summary>
        /// 失敗であることを表します。
        /// </summary>
        Failed,
    }

    /// <summary>
    /// ジョブの状態種別を定義します。
    /// </summary>
    /// <remarks><see cref="JobExecuteResult" /> によって取得できる、ジョブの状態です。</remarks>
    public enum JobStatus
    {
        /// <summary>
        /// 状態が不明な値であることを表します。
        /// </summary>
        None = 0,
        
        /// <summary>
        /// ジョブが開始されたことを表します。
        /// </summary>
        Start,
        
        /// <summary>
        /// ジョブが完了したことを表します。
        /// </summary>
        Success,
    }

    /// <summary>
    /// ログの出力レベルを定義します。
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// トレースログを表します。
        /// </summary>
        Trace,

        /// <summary>
        /// デバッグログを表します。
        /// </summary>
        Debug,

        /// <summary>
        /// 情報ログを表します。
        /// </summary>
        Information,

        /// <summary>
        /// 警告ログを表します。
        /// </summary>
        Warning,

        /// <summary>
        /// エラーログを表します。
        /// </summary>
        Error,

        /// <summary>
        /// 深刻なエラーログを表します。
        /// </summary>
        Fatal,
    }
}