// ReSharper disable InconsistentNaming
namespace JenkinsNotification.Core.Jenkins.Api
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Jobの実行結果クラスです。
    /// </summary>
    /// <remarks>
    /// WebSocketによるジョブ実行結果はこのクラスを器とします。
    /// </remarks>
    [DataContract]
    public class JobExecuteResult
    {
        /// <summary>
        /// ジョブ名称を設定、または取得します。
        /// </summary>
        [DataMember]
        public string project { get; set; }

        /// <summary>
        /// ジョブのビルド番号を設定、または取得します。
        /// </summary>
        [DataMember]
        public int number { get; set; }

        /// <summary>
        /// 状態を設定、または取得します。
        /// </summary>
        [DataMember]
        public string status { get; set; }

        /// <summary>
        /// 実行結果を設定、または取得します。
        /// </summary>
        [DataMember]
        public string result { get; set; }
    }
}