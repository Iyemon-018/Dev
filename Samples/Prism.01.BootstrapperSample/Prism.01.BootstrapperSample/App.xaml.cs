namespace Prism._01.BootstrapperSample
{
    using System.Windows;

    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        #region Methods

        /// <summary>
        /// <see cref="E:System.Windows.Application.Startup" /> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント データを格納している <see cref="T:System.Windows.StartupEventArgs" />。</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Bootstrapper の起動には、インスタンス化して.Run()を呼び出すだけ。
            // ただし、初期状態ではApp.xamlのMainWindowが設定されているので、これを消しておくこと。
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }

        #endregion
    }
}
