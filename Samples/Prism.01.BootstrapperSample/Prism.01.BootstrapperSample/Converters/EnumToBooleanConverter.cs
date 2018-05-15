namespace Prism._01.BootstrapperSample.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// 列挙体からbool型へのコンバータークラスです。
    /// </summary>
    /// <seealso cref="System.Windows.Data.IValueConverter" />
    [ValueConversion(typeof(Enum), typeof(bool), ParameterType = typeof(string))]
    public class EnumToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// 値を変換します。
        /// </summary>
        /// <param name="value">バインディング ソースによって生成された値。</param>
        /// <param name="targetType">バインディング ターゲット プロパティの型。</param>
        /// <param name="parameter">使用するコンバーター パラメーター。</param>
        /// <param name="culture">コンバーターで使用するカルチャ。</param>
        /// <returns>変換された値。メソッドが null を返す場合は、有効な null 値が使用されています。</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return DependencyProperty.UnsetValue;

            var enumString = parameter as string;
            var valueType = value.GetType();
            if (enumString == null) return DependencyProperty.UnsetValue;
            if (!Enum.IsDefined(valueType, value)) return DependencyProperty.UnsetValue;

            var enumValue = Enum.Parse(valueType, enumString);
            return enumValue.Equals(value);
        }

        /// <summary>
        /// 値を変換します。
        /// </summary>
        /// <param name="value">バインディング ターゲットによって生成される値。</param>
        /// <param name="targetType">変換後の型。</param>
        /// <param name="parameter">使用するコンバーター パラメーター。</param>
        /// <param name="culture">コンバーターで使用するカルチャ。</param>
        /// <returns>変換された値。メソッドが null を返す場合は、有効な null 値が使用されています。</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var enumString = parameter as string;
            if (enumString == null) return DependencyProperty.UnsetValue;
            
            return Enum.Parse(targetType, enumString);
        }
    }
}