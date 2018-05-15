using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using Google.Cloud.Vision.V1;

namespace GCP.VisionAPI.Sample.Converters
{
    [ValueConversion(typeof(BoundingPoly), typeof(string))]
    public class BoundingBoxToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BoundingPoly boundingPoly = value as BoundingPoly;
            if (boundingPoly == null)
            {
                return string.Empty;
            }

            string[] points = boundingPoly.Vertices
                                          .Select(v => new {x = v.X, y = v.Y})
                                          .Select(x => $"X:{x.x}, Y:{x.y}")
                                          .ToArray();
            return string.Join(" / ", points);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}