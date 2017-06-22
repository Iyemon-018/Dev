namespace WpfLinePerformance
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    public class VisualLineHost : FrameworkElement
    {
        private VisualCollection _children;

        private Point _previousPoint;

        private Pen _linePen;

        public VisualLineHost()
        {
            _children = new VisualCollection(this);
            _linePen = new Pen(Brushes.Black, 1);
        }

        public void PerformanceTest()
        {
            Random r = new Random();
            for (int i = 0; i < 10000; i++)
            {
                DrawingVisual dv = new DrawingVisual();
                Point currentPoint = new Point(r.NextDouble() * 1980, r.NextDouble() * 1080);
                using (DrawingContext dc = dv.RenderOpen())
                {
                    dc.DrawLine(_linePen, _previousPoint, currentPoint);
                }
                _children.Add(dv);
                _previousPoint = currentPoint;
            }
        }
        
        protected override int VisualChildrenCount => _children.Count;

        protected override Visual GetVisualChild(int index)
        {
            return _children[index];
        }
    }
}