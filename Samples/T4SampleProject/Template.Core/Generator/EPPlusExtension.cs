using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace Template.Core.Generator
{
    /// <summary>
    /// EPPlus の拡張メソッド機能を提供する。
    /// </summary>
    public static class EPPlusExtension
    {
        /// <summary>
        /// セルの値を文字列として取得する。
        /// </summary>
        /// <param name="range"></param>
        /// <returns>セル入力文字列</returns>
        public static string ValueString(this ExcelRangeBase range)
        {
            return range.Value == null ? string.Empty : range.Value.ToString();
        }

        /// <summary>
        /// セルが未入力かどうか。
        /// </summary>
        /// <param name="range"></param>
        /// <returns>true:未入力, false:入力済み</returns>
        public static bool IsEmpty(this ExcelRangeBase range)
        {
            return string.IsNullOrWhiteSpace(range.ValueString());
        }

        /// <summary>
        /// セルが入力済みかどうか。
        /// </summary>
        /// <param name="range"></param>
        /// <returns>true:入力済み, false:未入力</returns>
        public static bool IsInput(this ExcelRangeBase range)
        {
            return !range.IsEmpty();
        }
    }
}
