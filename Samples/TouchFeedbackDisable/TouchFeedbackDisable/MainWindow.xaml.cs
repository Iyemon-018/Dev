namespace TouchFeedbackDisable
{
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Shapes;

    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields

        private TouchPoint _point;

        #endregion

        #region Ctor

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void DrawLinePanel_OnPreviewTouchDown(object sender, TouchEventArgs e)
        {
            Canvas drawLinePanel = sender as Canvas;
            if (drawLinePanel != null)
            {
                _point = e.GetTouchPoint(drawLinePanel);
            }
        }

        private void DrawLinePanel_OnPreviewTouchMove(object sender, TouchEventArgs e)
        {
            Canvas drawLinePanel = sender as Canvas;
            if (drawLinePanel != null)
            {
                TouchPoint currentPoint = e.GetTouchPoint(drawLinePanel);
                Line line = new Line();
                line.Stroke = Brushes.White;
                line.StrokeThickness = 1;
                line.X1 = _point.Position.X;
                line.Y1 = _point.Position.Y;
                line.X2 = currentPoint.Position.X;
                line.Y2 = currentPoint.Position.Y;
                drawLinePanel.Children.Add(line);
                _point = currentPoint;
            }
        }

        private void TouchFeedback_OnChecked(object sender, RoutedEventArgs e)
        {
            TouchFeedbackStatus(true);
        }

        private void TouchFeedback_OnUnchecked(object sender, RoutedEventArgs e)
        {
            TouchFeedbackStatus(false);
        }

        private void TouchFeedbackStatus(bool enable)
        {
            Stylus.SetIsTouchFeedbackEnabled(this, enable);
            Stylus.SetIsTapFeedbackEnabled(this, enable);
            Stylus.SetIsPressAndHoldEnabled(this, enable);
            Cursor = enable ? Cursors.Arrow : Cursors.None;
        }

        #endregion
    }
}