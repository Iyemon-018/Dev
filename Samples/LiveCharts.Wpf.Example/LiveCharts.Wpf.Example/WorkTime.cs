namespace LiveCharts.Wpf.Example
{
    using System;
    using Prism.Mvvm;

    /// <summary>
    /// ある作業単位の実施時間を保持するクラスです。
    /// この作業実施時間は１日毎に異なる情報として扱われます。
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    public class WorkTime : BindableBase
    {
        private DateTime _date;

        /// <summary>
        /// 作業を実施した日付を設定、または取得します。
        /// </summary>
        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        private int _actualTime;

        /// <summary>
        /// 実作業時間を設定、または取得します。
        /// 単位は"秒"です。
        /// </summary>
        public int ActualTime
        {
            get => _actualTime;
            set => SetProperty(ref _actualTime, value);
        }

        private string _comment;

        /// <summary>
        /// 実施した作業内容を設定、または取得します。
        /// </summary>
        public string Comment
        {
            get => _comment;
            set => SetProperty(ref _comment, value);
        }

    }
}