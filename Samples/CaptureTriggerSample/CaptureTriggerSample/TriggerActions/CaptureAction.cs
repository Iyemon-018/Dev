namespace CaptureTriggerSample.TriggerActions
{
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Interactivity;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class CaptureAction : TriggerAction<FrameworkElement>
    {
        public static readonly DependencyProperty FileNameProperty =
                DependencyProperty.Register("FileName"
                                            , typeof(string)
                                            , typeof(CaptureAction)
                                            , new FrameworkPropertyMetadata(null));

        public string FileName
        {
            get => (string)GetValue(FileNameProperty);
            set => SetValue(FileNameProperty, value);
        }

        protected override void Invoke(object parameter)
        {
            Rect bounds = VisualTreeHelper.GetDescendantBounds(AssociatedObject);
            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                VisualBrush visualBrush = new VisualBrush(AssociatedObject);
                drawingContext.DrawRectangle(visualBrush, null, bounds);
            }

            RenderTargetBitmap renderTargetBitmap =
                    new RenderTargetBitmap((int)bounds.Width, (int)bounds.Height, 96, 96, PixelFormats.Pbgra32);
            renderTargetBitmap.Render(drawingVisual);
            renderTargetBitmap.Freeze();

            PngBitmapEncoder png = new PngBitmapEncoder();
            BitmapFrame bitmapFrame = BitmapFrame.Create(renderTargetBitmap);
            bitmapFrame.Freeze();
            png.Frames.Add(bitmapFrame);
            using (FileStream fileStream = File.Create(FileName))
            {
                png.Save(fileStream);
            }

            MessageBox.Show($"キャプチャーを保存しました{Environment.NewLine}"
                            + $"[ファイル名]{Environment.NewLine}"
                            + $" {FileName}"
                            , "キャプチャー"
                            , MessageBoxButton.OK
                            , MessageBoxImage.Information);
        }
    }
}