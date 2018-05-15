namespace JenkinsNotification.Core.ViewModels.Api
{
    using JenkinsNotification.Core.ComponentModels;

    /// <summary>
    /// ジョブ実行結果クラスです。
    /// </summary>
    /// <seealso cref="JenkinsNotification.Core.ViewModels.Api.IJobExecuteResult" />
    /// <seealso cref="JenkinsNotification.Core.ComponentModels.ViewModelBase" />
    /// <remarks><see cref="JenkinsNotification.Core.Jenkins.Api.JobExecuteResult" /> クラスの情報を保持します。</remarks>
    internal class JobExecuteResultViewModel : ViewModelBase, IJobExecuteResult
    {
        #region Fields

        /// <summary>
        /// ビルド番号
        /// </summary>
        private int _buildNumber;

        /// <summary>
        /// ジョブ名称
        /// </summary>
        private string _name;

        /// <summary>
        /// 実行結果
        /// </summary>
        private JobResultType _result;

        /// <summary>
        /// 状態
        /// </summary>
        private JobStatus _status;

        #endregion

        #region Properties

        /// <summary>
        /// ジョブ名称を取得します。
        /// </summary>
        public string Name
        {
            get { return _name; }
            internal set { SetProperty(ref _name, value); }
        }

        /// <summary>
        /// ビルド番号を取得します。
        /// </summary>
        public int BuildNumber
        {
            get { return _buildNumber; }
            internal set { SetProperty(ref _buildNumber, value); }
        }

        /// <summary>
        /// 状態を取得します。
        /// </summary>
        public JobStatus Status
        {
            get { return _status; }
            internal set { SetProperty(ref _status, value); }
        }

        /// <summary>
        /// 実行結果を取得します。
        /// </summary>
        public JobResultType Result
        {
            get { return _result; }
            internal set { SetProperty(ref _result, value); }
        }

        #endregion
    }
}