namespace Prism._01.BootstrapperSample
{
    using System.Windows;
    using Microsoft.Practices.Prism.Mvvm;
    using Prism._01.BootstrapperSample.Attributes;

    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    [ViewModel(typeof(MainWindowViewModel))]
    public partial class MainWindow : Window, IView
    {
        #region Ctor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion
    }
}
