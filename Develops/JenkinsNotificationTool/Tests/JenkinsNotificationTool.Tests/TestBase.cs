namespace JenkinsNotificationTool.Tests
{
    using Xunit.Abstractions;

    /// <summary>
    /// テストで使用する機能を包含したすべてのテストクラスの基底クラスです。
    /// </summary>
    public abstract class TestBase
    {
        #region Fields

        /// <summary>
        /// テスト出力ヘルパー
        /// </summary>
        private readonly ITestOutputHelper _output;

        #endregion

        #region Ctor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="output">テスト出力ヘルパー</param>
        protected TestBase(ITestOutputHelper output)
        {
            _output = output;
        }

        #endregion

        #region Properties

        /// <summary>
        /// テスト出力ヘルパーを取得します。
        /// </summary>
        protected ITestOutputHelper Output => _output;

        #endregion
    }
}