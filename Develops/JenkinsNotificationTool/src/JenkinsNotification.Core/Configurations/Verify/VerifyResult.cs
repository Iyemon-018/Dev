namespace JenkinsNotification.Core.Configurations.Verify
{
    /// <summary>
    /// 構成情報の検証結果クラスです。
    /// </summary>
    public class VerifyResult
    {
        #region Ctor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public VerifyResult() : this(true, string.Empty)
        {
            
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="correct">検証結果(true:正常, false:異常)</param>
        /// <param name="errorMessage">エラーメッセージ</param>
        internal VerifyResult(bool correct, string errorMessage)
        {
            Correct = correct;
            ErrorMessage = errorMessage;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 検証結果が正常だったかどうかを取得します。
        /// </summary>
        public bool Correct { get; private set; }

        /// <summary>
        /// 検証結果が異常だった場合のエラーメッセージを取得します。
        /// </summary>
        public string ErrorMessage { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// 検証異常の結果オブジェクトを取得します。
        /// </summary>
        /// <param name="errorMessage">エラーメッセージ</param>
        /// <returns>検証異常の結果オブジェクト</returns>
        public static VerifyResult Error(string errorMessage)
        {
            return new VerifyResult(false, errorMessage);
        }

        #endregion
    }
}