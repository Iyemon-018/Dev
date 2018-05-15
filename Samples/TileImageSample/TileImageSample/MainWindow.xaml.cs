namespace TileImageSample
{
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields

        private Point _point;

        #endregion

        #region Ctor

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Rect viewport = ImageTileBrush.Viewport;
            viewport.Offset(10000000, 10000000);
            ImageTileBrush.Viewport = viewport;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _point = e.GetPosition(this);
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point point = e.GetPosition(this);
                Vector moveVector = new Vector(point.X - _point.X, point.Y - _point.Y);
                Rect viewport = ImageTileBrush.Viewport;
                viewport.Offset(moveVector);
                ImageTileBrush.Viewport = viewport;
                _point = point;
            }
        }

        #endregion
    }
}
