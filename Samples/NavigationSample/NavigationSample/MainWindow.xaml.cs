namespace NavigationSample
{
    using System.Windows;
    using NavigationSample.ViewModels;

    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    [ViewModelResolve(typeof(MainWindowViewModel))]
    public partial class MainWindow : Window
    {
        #region Ctor

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion
    }
}
