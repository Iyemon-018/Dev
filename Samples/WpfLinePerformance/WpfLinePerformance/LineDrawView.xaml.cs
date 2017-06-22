using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfLinePerformance
{
    using Core;

    /// <summary>
    /// LineDrawView.xaml の相互作用ロジック
    /// </summary>
    public partial class LineDrawView : Window
    {
        public LineDrawView()
        {
            InitializeComponent();

            using (PerformanceTracer.StartNew("Line Drawing"))
            {
                PerformanceTest();
            }
        }

        private Point _previousPoint;

        private void PerformanceTest()
        {
            Random r = new Random();
            for (int i = 0; i < 10000; i++)
            {
                Point currentPoint = new Point(r.NextDouble() * 1980, r.NextDouble() * 1080);
                Line line = new Line
                            {
                                X1 = _previousPoint.X,
                                Y1 = _previousPoint.Y,
                                X2 = currentPoint.X,
                                Y2 = currentPoint.Y,
                                Stroke = Brushes.Black,
                                StrokeThickness = 1,
                            };
                LayoutRoot.Children.Add(line);
                _previousPoint = currentPoint;
            }
        }
    }
}
