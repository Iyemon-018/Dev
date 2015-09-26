using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;

namespace AutoMapperSample
{
    /// <summary>
    /// マッピング元のオブジェクト
    /// </summary>
    public class Source : BindableBase
    {
        #region プロパティ

        private int _number;

        /// <summary>数値</summary>
        public int Number
        {
            get { return _number; }
            set { SetProperty<int>(ref _number, value); }
        }

        private string _text;

        /// <summary>文字列</summary>
        public string Text
        {
            get { return _text; }
            set { SetProperty<string>(ref _text, value); }
        }

        private DayOfWeek _dayOfWeek;

        /// <summary>列挙型</summary>
        public DayOfWeek DayOfWeek
        {
            get { return _dayOfWeek; }
            set { SetProperty<DayOfWeek>(ref _dayOfWeek, value); }
        }

        private string _remarks;

        /// <summary>備考</summary>
        public string Remarks
        {
            get { return _remarks; }
            set { SetProperty<string>(ref _remarks, value); }
        }

        #endregion //プロパティ

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(this.GetType().ToString());
            sb.AppendLine(string.Format(" Number : {0}", this.Number));
            sb.AppendLine(string.Format(" Text : {0}", this.Text));
            sb.AppendLine(string.Format(" DayOfWeek : {0}", this.DayOfWeek));
            sb.AppendLine(string.Format(" Remarks : {0}", this.Remarks));

            return sb.ToString();
        }
    }
}
