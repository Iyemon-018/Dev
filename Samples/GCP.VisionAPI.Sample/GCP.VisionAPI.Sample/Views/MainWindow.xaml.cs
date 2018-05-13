using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Google.Cloud.Vision.V1;

namespace GCP.VisionAPI.Sample.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ViewBoundingBoxArea(BoundingPoly blockBoundingBox)
        {
            ViewRectanglePanel.Children.Clear();

            var topLeft = blockBoundingBox.Vertices[0];
            var topRight = blockBoundingBox.Vertices[1];
            var bottomRight = blockBoundingBox.Vertices[2];
            var bottomLeft = blockBoundingBox.Vertices[3];

            var rectangle = new Rectangle
            {
                Width = topRight.X - topLeft.X,
                Height = bottomLeft.Y - topLeft.Y,
                StrokeThickness = 2.0,
                Stroke = Brushes.Aqua,
            };

            Canvas.SetLeft(rectangle, topLeft.X);
            Canvas.SetTop(rectangle, topLeft.Y);
            ViewRectanglePanel.Children.Add(rectangle);
        }

        private void ViewRectangleButton_OnClick(object sender, RoutedEventArgs e)
        {
            var control = sender as Control;
            if (control == null) return;

            var block = control.DataContext as Block;
            if (block != null)
            {
                ViewBoundingBoxArea(block.BoundingBox);
            }

            var paragraph = control.DataContext as Paragraph;
            if (paragraph != null)
            {
                ViewBoundingBoxArea(paragraph.BoundingBox);
            }

            var word = control.DataContext as Word;
            if (word != null)
            {
                ViewBoundingBoxArea(word.BoundingBox);
            }

            var symbol = control.DataContext as Symbol;
            if (symbol != null)
            {
                ViewBoundingBoxArea(symbol.BoundingBox);
            }
        }
    }
}
