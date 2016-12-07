namespace JenkinsNotification.Core.Extensions
{
    using System;
    using System.Data.Common;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// <see cref="string"/> 型の拡張メソッドを定義します。
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 文字列が空文字かどうかを判定します。
        /// </summary>
        /// <param name="self">自分自身</param>
        /// <returns>空文字、もしくはnull の場合、true を返します。それ以外の場合、false を返します。</returns>
        public static bool IsEmpty(this string self)
        {
            return string.IsNullOrEmpty(self);
        }

        /// <summary>
        /// 文字列が設定されているかどうかを判定します。
        /// </summary>
        /// <param name="self">自分自身</param>
        /// <returns>文字列が設定されている場合、true を返します。それ以外の場合、false を返します。</returns>
        public static bool HasText(this string self)
        {
            return !self.IsEmpty();
        }

        /// <summary>
        /// 値が<typeparamref name="TEnum"/> 型の列挙値として定義されているかどうかを判定します。<para/>
        /// 値が空文字、あるいはnull の場合は、false を返します。
        /// </summary>
        /// <typeparam name="TEnum">判定対象の列挙体の型</typeparam>
        /// <param name="self">自分自身</param>
        /// <returns>判定結果(true:定義済み, false:未定義)</returns>
        public static bool IsDefined<TEnum>(this string self) where TEnum : struct
        {
            return !self.IsEmpty() && Enum.IsDefined(typeof(TEnum), self);
        }

        /// <summary>
        /// 値を<typeparamref name="TEnum"/> 型に変換します。<para/>
        /// 値が未定義だったり変換に失敗（値が空文字、null の場合など）した場合は、<paramref name="defaultValue"/> を返します。
        /// </summary>
        /// <typeparam name="TEnum">変換する列挙体の型</typeparam>
        /// <param name="self">自分自身</param>
        /// <param name="defaultValue">変換に失敗した場合の戻り値</param>
        /// <returns>変換結果の値</returns>
        public static TEnum ToEnum<TEnum>(this string self, TEnum defaultValue = default(TEnum)) where TEnum : struct
        {
            TEnum result;
            return self.IsDefined<TEnum>()
                ? Enum.TryParse(self, out result) ? result : defaultValue
                : defaultValue;
        }

        /// <summary>
        /// 文字列が数値かどうかを判定します。
        /// </summary>
        /// <param name="self">自分自身</param>
        /// <param name="allowDecimal">小数を許容するかどうか</param>
        /// <param name="allowLeadingSigne">先頭の±記号を許容するかどうか</param>
        /// <param name="allowThousands">千の位ごとのカンマを許容するかどうか</param>
        /// <returns>判定結果(true:文字列は数値です。, false:文字列は数値以外を含みます。)</returns>
        /// <remarks>
        /// <see cref="allowDecimal"/> がtrue の場合、文字列に小数点が含まれていても数値として扱います。
        /// ただし、"小数点のみ"、"小数が複数"など、数値とみなされない場合もfalse を返します。<para/><para/>
        /// 
        /// <see cref="allowLeadingSigne"/> がtrue の場合、文字列の先頭が±記号であっても数値として扱います。
        /// ただし、"末尾に記号"、"±以外の記号"が含まれる場合は、false を返します。<para/><para/>
        /// 
        /// <see cref="allowThousands"/> がtrue の場合、文字列に千の位のカンマが含まれていても数値として扱います。
        /// </remarks>
        public static bool IsNumeric(this string self, bool allowDecimal, bool allowLeadingSigne, bool allowThousands)
        {
            var numberStyles = NumberStyles.None;
            if (allowDecimal) numberStyles |= NumberStyles.AllowDecimalPoint;
            if (allowLeadingSigne) numberStyles |= NumberStyles.AllowLeadingSign;
            if (allowThousands) numberStyles |= NumberStyles.AllowThousands;

            double result;
            var isNumeric = double.TryParse(self, numberStyles, null, out result);
            return isNumeric;
        }

        /// <summary>
        /// DBコマンド<see cref="DbCommand"/> からSQLクエリ文字列を生成します。
        /// </summary>
        /// <param name="self">自分自身</param>
        /// <returns>ログ出力用のSQLクエリ文字列</returns>
        public static string SqlLogText(this DbCommand self)
        {
            var sb = new StringBuilder();
            sb.AppendLine(self.CommandText);

            foreach (DbParameter parameter in self.Parameters)
            {
                sb.AppendLine($"-- @{parameter.ParameterName}: {parameter.DbType} [{parameter.Value}]");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 文字列を整数値に変換します。
        /// </summary>
        /// <param name="self">自分自身</param>
        /// <param name="defaultValue">変換失敗時の戻り値</param>
        /// <returns>変換結果</returns>
        public static int ToInt(this string self, int defaultValue = default(int))
        {
            int result;
            return int.TryParse(self, out result) ? result : defaultValue;
        }
    }
}