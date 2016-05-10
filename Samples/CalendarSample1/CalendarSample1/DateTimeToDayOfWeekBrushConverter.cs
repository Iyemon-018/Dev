namespace CalendarSample1
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Media;

    /// <summary>
    /// <see cref="T:DateTime"/>の値から曜日の<see cref="T:Brush"/>リソースへのコンバータークラスです。
    /// </summary>
    /// <seealso cref="System.Windows.Data.IValueConverter" />
    [ValueConversion(typeof(DateTime), typeof(Brush))]
    public class DateTimeToDayOfWeekBrushConverter : IValueConverter
    {
        /// <summary>
        /// 日曜日用のブラシリソース
        /// </summary>
        public static Brush SundayBrush
        {
            get { return Brushes.Red; }
        }

        /// <summary>
        /// 土曜日用のブラシリソース
        /// </summary>
        public static Brush SaturdayBrush
        {
            get { return Brushes.Blue; }
        }

        /// <summary>値を変換します。</summary>
        /// <returns>変換された値。 メソッドが null を返す場合は、有効な null 値が使用されています。</returns>
        /// <param name="value">バインディング ソースによって生成された値。</param>
        /// <param name="targetType">バインディング ターゲット プロパティの型。</param>
        /// <param name="parameter">使用するコンバーター パラメーター。</param>
        /// <param name="culture">コンバーターで使用するカルチャ。</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DateTime))
            {
                return DependencyProperty.UnsetValue;
            }

            var dayOfWeek = ((DateTime)value).DayOfWeek;
            switch (dayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return SundayBrush;
                case DayOfWeek.Saturday:
                    return SaturdayBrush;
                default:
                    return DependencyProperty.UnsetValue;
            }
        }

        /// <summary>値を変換します。</summary>
        /// <returns>変換された値。 メソッドが null を返す場合は、有効な null 値が使用されています。</returns>
        /// <param name="value">バインディング ターゲットによって生成される値。</param>
        /// <param name="targetType">変換後の型。</param>
        /// <param name="parameter">使用するコンバーター パラメーター。</param>
        /// <param name="culture">コンバーターで使用するカルチャ。</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}