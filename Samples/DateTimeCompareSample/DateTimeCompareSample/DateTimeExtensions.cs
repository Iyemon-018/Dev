using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeCompareSample
{
    /// <summary>
    /// 日時種別
    /// </summary>
    public enum DateTimeKind
    {
        /// <summary>年</summary>
        Year,
        /// <summary>月</summary>
        Month,
        /// <summary>日</summary>
        Day,
        /// <summary>時間</summary>
        Hour,
        /// <summary>分</summary>
        Minute,
        /// <summary>秒</summary>
        Second,
        /// <summary>ミリ秒</summary>
        Milliseconds,
    }

    /// <summary>
    /// DateTime型の拡張メソッドを定義する。
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 現在のインスタンスと比較します。
        /// </summary>
        /// <param name="self">自分自身</param>
        /// <param name="value">比較する値</param>
        /// <param name="compareKind">比較する時間の種別</param>
        /// <returns>比較結果(true:一致, false:不一致)</returns>
        public static bool Compare(this DateTime self, DateTime value, DateTimeKind compareKind)
        {
            var ts = self - value;
            double total = 0D;
            double sub = 1.0D;

            switch (compareKind)
            {
                case DateTimeKind.Year:
                    total = self.Year - value.Year;
                    break;
                case DateTimeKind.Month:
                    sub = 30.0D;
                    total = ts.TotalDays;
                    break;
                case DateTimeKind.Day:
                    total = ts.TotalDays;
                    break;
                case DateTimeKind.Hour:
                    total = ts.TotalHours;
                    break;
                case DateTimeKind.Minute:
                    total = ts.TotalMinutes;
                    break;
                case DateTimeKind.Second:
                    total = ts.TotalSeconds;
                    break;
                case DateTimeKind.Milliseconds:
                    total = ts.TotalMilliseconds;
                    break;
            }

            return Math.Abs(total) < sub;
        }

        /// <summary>
        /// 現在のインスタンスと年を比較する。
        /// </summary>
        /// <param name="self">自分自身</param>
        /// <param name="value">比較する値</param>
        /// <returns>比較結果(true:一致, false:不一致)</returns>
        public static bool CompareYear(this DateTime self, DateTime value)
        {
            return self.Compare(value, DateTimeKind.Year);
        }

        /// <summary>
        /// 現在のインスタンスと月を比較する。
        /// </summary>
        /// <param name="self">自分自身</param>
        /// <param name="value">比較する値</param>
        /// <returns>比較結果(true:一致, false:不一致)</returns>
        public static bool CompareMonth(this DateTime self, DateTime value)
        {
            return self.Compare(value, DateTimeKind.Month);
        }

        /// <summary>
        /// 現在のインスタンスと日を比較する。
        /// </summary>
        /// <param name="self">自分自身</param>
        /// <param name="value">比較する値</param>
        /// <returns>比較結果(true:一致, false:不一致)</returns>
        public static bool CompareDay(this DateTime self, DateTime value)
        {
            return self.Compare(value, DateTimeKind.Day);
        }

        /// <summary>
        /// 現在のインスタンスと時間を比較する。
        /// </summary>
        /// <param name="self">自分自身</param>
        /// <param name="value">比較する値</param>
        /// <returns>比較結果(true:一致, false:不一致)</returns>
        public static bool CompareHour(this DateTime self, DateTime value)
        {
            return self.Compare(value, DateTimeKind.Hour);
        }

        /// <summary>
        /// 現在のインスタンスと分を比較する。
        /// </summary>
        /// <param name="self">自分自身</param>
        /// <param name="value">比較する値</param>
        /// <returns>比較結果(true:一致, false:不一致)</returns>
        public static bool CompareMinute(this DateTime self, DateTime value)
        {
            return self.Compare(value, DateTimeKind.Minute);
        }

        /// <summary>
        /// 現在のインスタンスと秒を比較する。
        /// </summary>
        /// <param name="self">自分自身</param>
        /// <param name="value">比較する値</param>
        /// <returns>比較結果(true:一致, false:不一致)</returns>
        public static bool CompareSecond(this DateTime self, DateTime value)
        {
            return self.Compare(value, DateTimeKind.Second);
        }

        /// <summary>
        /// 現在のインスタンスとミリ秒を比較する。
        /// </summary>
        /// <param name="self">自分自身</param>
        /// <param name="value">比較する値</param>
        /// <returns>比較結果(true:一致, false:不一致)</returns>
        public static bool CompareMillisecond(this DateTime self, DateTime value)
        {
            return self.Compare(value, DateTimeKind.Milliseconds);
        }
    }
}
